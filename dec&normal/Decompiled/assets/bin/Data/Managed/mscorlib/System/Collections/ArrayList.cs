using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace System.Collections
{
	// Token: 0x02000077 RID: 119
	[DebuggerTypeProxy(typeof(CollectionDebuggerView))]
	[DebuggerDisplay("Count={Count}")]
	[ComVisible(true)]
	[Serializable]
	public class ArrayList : ICollection, IEnumerable, IList, ICloneable
	{
		// Token: 0x060003CA RID: 970 RVA: 0x00012960 File Offset: 0x00010B60
		public ArrayList()
		{
			this._items = ArrayList.EmptyArray;
		}

		// Token: 0x060003CB RID: 971 RVA: 0x00012974 File Offset: 0x00010B74
		public ArrayList(ICollection c)
		{
			if (c == null)
			{
				throw new ArgumentNullException("c");
			}
			Array array = c as Array;
			if (array != null && array.Rank != 1)
			{
				throw new RankException();
			}
			this._items = new object[c.Count];
			this.AddRange(c);
		}

		// Token: 0x060003CC RID: 972 RVA: 0x000129D0 File Offset: 0x00010BD0
		public ArrayList(int capacity)
		{
			if (capacity < 0)
			{
				ArrayList.ThrowNewArgumentOutOfRangeException("capacity", capacity, "The initial capacity can't be smaller than zero.");
			}
			if (capacity == 0)
			{
				capacity = 4;
			}
			this._items = new object[capacity];
		}

		// Token: 0x060003CD RID: 973 RVA: 0x00012A0C File Offset: 0x00010C0C
		private ArrayList(object[] array, int index, int count)
		{
			if (count == 0)
			{
				this._items = new object[4];
			}
			else
			{
				this._items = new object[count];
			}
			Array.Copy(array, index, this._items, 0, count);
			this._size = count;
		}

		// Token: 0x1700005B RID: 91
		public virtual object this[int index]
		{
			get
			{
				if (index < 0 || index >= this._size)
				{
					ArrayList.ThrowNewArgumentOutOfRangeException("index", index, "Index is less than 0 or more than or equal to the list count.");
				}
				return this._items[index];
			}
			set
			{
				if (index < 0 || index >= this._size)
				{
					ArrayList.ThrowNewArgumentOutOfRangeException("index", index, "Index is less than 0 or more than or equal to the list count.");
				}
				this._items[index] = value;
				this._version++;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060003D1 RID: 977 RVA: 0x00012AE8 File Offset: 0x00010CE8
		public virtual int Count
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x00012AF0 File Offset: 0x00010CF0
		// (set) Token: 0x060003D3 RID: 979 RVA: 0x00012AFC File Offset: 0x00010CFC
		public virtual int Capacity
		{
			get
			{
				return this._items.Length;
			}
			set
			{
				if (value < this._size)
				{
					ArrayList.ThrowNewArgumentOutOfRangeException("Capacity", value, "Must be more than count.");
				}
				object[] array = new object[value];
				Array.Copy(this._items, 0, array, 0, this._size);
				this._items = array;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x00012B4C File Offset: 0x00010D4C
		public virtual bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060003D5 RID: 981 RVA: 0x00012B50 File Offset: 0x00010D50
		public virtual bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x00012B54 File Offset: 0x00010D54
		public virtual bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x00012B58 File Offset: 0x00010D58
		public virtual object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x00012B5C File Offset: 0x00010D5C
		private void EnsureCapacity(int count)
		{
			if (count <= this._items.Length)
			{
				return;
			}
			int i = this._items.Length << 1;
			if (i == 0)
			{
				i = 4;
			}
			while (i < count)
			{
				i <<= 1;
			}
			object[] array = new object[i];
			Array.Copy(this._items, 0, array, 0, this._items.Length);
			this._items = array;
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x00012BC0 File Offset: 0x00010DC0
		private void Shift(int index, int count)
		{
			if (count > 0)
			{
				if (this._size + count > this._items.Length)
				{
					int i;
					for (i = ((this._items.Length <= 0) ? 1 : (this._items.Length << 1)); i < this._size + count; i <<= 1)
					{
					}
					object[] array = new object[i];
					Array.Copy(this._items, 0, array, 0, index);
					Array.Copy(this._items, index, array, index + count, this._size - index);
					this._items = array;
				}
				else
				{
					Array.Copy(this._items, index, this._items, index + count, this._size - index);
				}
			}
			else if (count < 0)
			{
				int num = index - count;
				Array.Copy(this._items, num, this._items, index, this._size - num);
				Array.Clear(this._items, this._size + count, -count);
			}
		}

		// Token: 0x060003DA RID: 986 RVA: 0x00012CB8 File Offset: 0x00010EB8
		public virtual int Add(object value)
		{
			if (this._items.Length <= this._size)
			{
				this.EnsureCapacity(this._size + 1);
			}
			this._items[this._size] = value;
			this._version++;
			return this._size++;
		}

		// Token: 0x060003DB RID: 987 RVA: 0x00012D14 File Offset: 0x00010F14
		public virtual void Clear()
		{
			Array.Clear(this._items, 0, this._size);
			this._size = 0;
			this._version++;
		}

		// Token: 0x060003DC RID: 988 RVA: 0x00012D40 File Offset: 0x00010F40
		public virtual bool Contains(object item)
		{
			return this.IndexOf(item, 0, this._size) > -1;
		}

		// Token: 0x060003DD RID: 989 RVA: 0x00012D54 File Offset: 0x00010F54
		public virtual int IndexOf(object value)
		{
			return this.IndexOf(value, 0);
		}

		// Token: 0x060003DE RID: 990 RVA: 0x00012D60 File Offset: 0x00010F60
		public virtual int IndexOf(object value, int startIndex)
		{
			return this.IndexOf(value, startIndex, this._size - startIndex);
		}

		// Token: 0x060003DF RID: 991 RVA: 0x00012D74 File Offset: 0x00010F74
		public virtual int IndexOf(object value, int startIndex, int count)
		{
			if (startIndex < 0 || startIndex > this._size)
			{
				ArrayList.ThrowNewArgumentOutOfRangeException("startIndex", startIndex, "Does not specify valid index.");
			}
			if (count < 0)
			{
				ArrayList.ThrowNewArgumentOutOfRangeException("count", count, "Can't be less than 0.");
			}
			if (startIndex > this._size - count)
			{
				throw new ArgumentOutOfRangeException("count", "Start index and count do not specify a valid range.");
			}
			return Array.IndexOf<object>(this._items, value, startIndex, count);
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x00012DF4 File Offset: 0x00010FF4
		public virtual void Insert(int index, object value)
		{
			if (index < 0 || index > this._size)
			{
				ArrayList.ThrowNewArgumentOutOfRangeException("index", index, "Index must be >= 0 and <= Count.");
			}
			this.Shift(index, 1);
			this._items[index] = value;
			this._size++;
			this._version++;
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x00012E58 File Offset: 0x00011058
		public virtual void InsertRange(int index, ICollection c)
		{
			if (c == null)
			{
				throw new ArgumentNullException("c");
			}
			if (index < 0 || index > this._size)
			{
				ArrayList.ThrowNewArgumentOutOfRangeException("index", index, "Index must be >= 0 and <= Count.");
			}
			int count = c.Count;
			if (this._items.Length < this._size + count)
			{
				this.EnsureCapacity(this._size + count);
			}
			if (index < this._size)
			{
				Array.Copy(this._items, index, this._items, index + count, this._size - index);
			}
			if (this == c.SyncRoot)
			{
				Array.Copy(this._items, 0, this._items, index, index);
				Array.Copy(this._items, index + count, this._items, index << 1, this._size - index);
			}
			else
			{
				c.CopyTo(this._items, index);
			}
			this._size += c.Count;
			this._version++;
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x00012F64 File Offset: 0x00011164
		public virtual void Remove(object obj)
		{
			int num = this.IndexOf(obj);
			if (num > -1)
			{
				this.RemoveAt(num);
			}
			this._version++;
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x00012F98 File Offset: 0x00011198
		public virtual void RemoveAt(int index)
		{
			if (index < 0 || index >= this._size)
			{
				ArrayList.ThrowNewArgumentOutOfRangeException("index", index, "Less than 0 or more than list count.");
			}
			this.Shift(index, -1);
			this._size--;
			this._version++;
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x00012FF4 File Offset: 0x000111F4
		public virtual void CopyTo(Array array)
		{
			Array.Copy(this._items, array, this._size);
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x00013008 File Offset: 0x00011208
		public virtual void CopyTo(Array array, int arrayIndex)
		{
			this.CopyTo(0, array, arrayIndex, this._size);
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0001301C File Offset: 0x0001121C
		public virtual void CopyTo(int index, Array array, int arrayIndex, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException("Must have only 1 dimensions.", "array");
			}
			Array.Copy(this._items, index, array, arrayIndex, count);
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x0001305C File Offset: 0x0001125C
		public virtual IEnumerator GetEnumerator()
		{
			return new ArrayList.SimpleEnumerator(this);
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x00013064 File Offset: 0x00011264
		public virtual void AddRange(ICollection c)
		{
			this.InsertRange(this._size, c);
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x00013074 File Offset: 0x00011274
		public virtual void Sort()
		{
			Array.Sort<object>(this._items, 0, this._size);
			this._version++;
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x00013098 File Offset: 0x00011298
		public virtual void Sort(IComparer comparer)
		{
			Array.Sort(this._items, 0, this._size, comparer);
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x000130B0 File Offset: 0x000112B0
		public virtual object[] ToArray()
		{
			object[] array = new object[this._size];
			this.CopyTo(array);
			return array;
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x000130D4 File Offset: 0x000112D4
		public virtual Array ToArray(Type type)
		{
			Array array = Array.CreateInstance(type, this._size);
			this.CopyTo(array);
			return array;
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x000130F8 File Offset: 0x000112F8
		public virtual object Clone()
		{
			return new ArrayList(this._items, 0, this._size);
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0001310C File Offset: 0x0001130C
		internal static void ThrowNewArgumentOutOfRangeException(string name, object actual, string message)
		{
			throw new ArgumentOutOfRangeException(name, actual, message);
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x00013118 File Offset: 0x00011318
		public static ArrayList Synchronized(ArrayList list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			if (list.IsSynchronized)
			{
				return list;
			}
			return new ArrayList.SynchronizedArrayListWrapper(list);
		}

		// Token: 0x040001C7 RID: 455
		private const int DefaultInitialCapacity = 4;

		// Token: 0x040001C8 RID: 456
		private int _size;

		// Token: 0x040001C9 RID: 457
		private object[] _items;

		// Token: 0x040001CA RID: 458
		private int _version;

		// Token: 0x040001CB RID: 459
		private static readonly object[] EmptyArray = new object[0];

		// Token: 0x02000078 RID: 120
		[Serializable]
		private class ArrayListWrapper : ArrayList
		{
			// Token: 0x060003F0 RID: 1008 RVA: 0x00013140 File Offset: 0x00011340
			public ArrayListWrapper(ArrayList innerArrayList)
			{
				this.m_InnerArrayList = innerArrayList;
			}

			// Token: 0x17000062 RID: 98
			public override object this[int index]
			{
				get
				{
					return this.m_InnerArrayList[index];
				}
				set
				{
					this.m_InnerArrayList[index] = value;
				}
			}

			// Token: 0x17000063 RID: 99
			// (get) Token: 0x060003F3 RID: 1011 RVA: 0x00013170 File Offset: 0x00011370
			public override int Count
			{
				get
				{
					return this.m_InnerArrayList.Count;
				}
			}

			// Token: 0x17000064 RID: 100
			// (get) Token: 0x060003F4 RID: 1012 RVA: 0x00013180 File Offset: 0x00011380
			// (set) Token: 0x060003F5 RID: 1013 RVA: 0x00013190 File Offset: 0x00011390
			public override int Capacity
			{
				get
				{
					return this.m_InnerArrayList.Capacity;
				}
				set
				{
					this.m_InnerArrayList.Capacity = value;
				}
			}

			// Token: 0x17000065 RID: 101
			// (get) Token: 0x060003F6 RID: 1014 RVA: 0x000131A0 File Offset: 0x000113A0
			public override bool IsFixedSize
			{
				get
				{
					return this.m_InnerArrayList.IsFixedSize;
				}
			}

			// Token: 0x17000066 RID: 102
			// (get) Token: 0x060003F7 RID: 1015 RVA: 0x000131B0 File Offset: 0x000113B0
			public override bool IsReadOnly
			{
				get
				{
					return this.m_InnerArrayList.IsReadOnly;
				}
			}

			// Token: 0x17000067 RID: 103
			// (get) Token: 0x060003F8 RID: 1016 RVA: 0x000131C0 File Offset: 0x000113C0
			public override bool IsSynchronized
			{
				get
				{
					return this.m_InnerArrayList.IsSynchronized;
				}
			}

			// Token: 0x17000068 RID: 104
			// (get) Token: 0x060003F9 RID: 1017 RVA: 0x000131D0 File Offset: 0x000113D0
			public override object SyncRoot
			{
				get
				{
					return this.m_InnerArrayList.SyncRoot;
				}
			}

			// Token: 0x060003FA RID: 1018 RVA: 0x000131E0 File Offset: 0x000113E0
			public override int Add(object value)
			{
				return this.m_InnerArrayList.Add(value);
			}

			// Token: 0x060003FB RID: 1019 RVA: 0x000131F0 File Offset: 0x000113F0
			public override void Clear()
			{
				this.m_InnerArrayList.Clear();
			}

			// Token: 0x060003FC RID: 1020 RVA: 0x00013200 File Offset: 0x00011400
			public override bool Contains(object value)
			{
				return this.m_InnerArrayList.Contains(value);
			}

			// Token: 0x060003FD RID: 1021 RVA: 0x00013210 File Offset: 0x00011410
			public override int IndexOf(object value)
			{
				return this.m_InnerArrayList.IndexOf(value);
			}

			// Token: 0x060003FE RID: 1022 RVA: 0x00013220 File Offset: 0x00011420
			public override int IndexOf(object value, int startIndex)
			{
				return this.m_InnerArrayList.IndexOf(value, startIndex);
			}

			// Token: 0x060003FF RID: 1023 RVA: 0x00013230 File Offset: 0x00011430
			public override int IndexOf(object value, int startIndex, int count)
			{
				return this.m_InnerArrayList.IndexOf(value, startIndex, count);
			}

			// Token: 0x06000400 RID: 1024 RVA: 0x00013240 File Offset: 0x00011440
			public override void Insert(int index, object value)
			{
				this.m_InnerArrayList.Insert(index, value);
			}

			// Token: 0x06000401 RID: 1025 RVA: 0x00013250 File Offset: 0x00011450
			public override void InsertRange(int index, ICollection c)
			{
				this.m_InnerArrayList.InsertRange(index, c);
			}

			// Token: 0x06000402 RID: 1026 RVA: 0x00013260 File Offset: 0x00011460
			public override void Remove(object value)
			{
				this.m_InnerArrayList.Remove(value);
			}

			// Token: 0x06000403 RID: 1027 RVA: 0x00013270 File Offset: 0x00011470
			public override void RemoveAt(int index)
			{
				this.m_InnerArrayList.RemoveAt(index);
			}

			// Token: 0x06000404 RID: 1028 RVA: 0x00013280 File Offset: 0x00011480
			public override void CopyTo(Array array)
			{
				this.m_InnerArrayList.CopyTo(array);
			}

			// Token: 0x06000405 RID: 1029 RVA: 0x00013290 File Offset: 0x00011490
			public override void CopyTo(Array array, int index)
			{
				this.m_InnerArrayList.CopyTo(array, index);
			}

			// Token: 0x06000406 RID: 1030 RVA: 0x000132A0 File Offset: 0x000114A0
			public override void CopyTo(int index, Array array, int arrayIndex, int count)
			{
				this.m_InnerArrayList.CopyTo(index, array, arrayIndex, count);
			}

			// Token: 0x06000407 RID: 1031 RVA: 0x000132B4 File Offset: 0x000114B4
			public override IEnumerator GetEnumerator()
			{
				return this.m_InnerArrayList.GetEnumerator();
			}

			// Token: 0x06000408 RID: 1032 RVA: 0x000132C4 File Offset: 0x000114C4
			public override void AddRange(ICollection c)
			{
				this.m_InnerArrayList.AddRange(c);
			}

			// Token: 0x06000409 RID: 1033 RVA: 0x000132D4 File Offset: 0x000114D4
			public override object Clone()
			{
				return this.m_InnerArrayList.Clone();
			}

			// Token: 0x0600040A RID: 1034 RVA: 0x000132E4 File Offset: 0x000114E4
			public override void Sort()
			{
				this.m_InnerArrayList.Sort();
			}

			// Token: 0x0600040B RID: 1035 RVA: 0x000132F4 File Offset: 0x000114F4
			public override void Sort(IComparer comparer)
			{
				this.m_InnerArrayList.Sort(comparer);
			}

			// Token: 0x0600040C RID: 1036 RVA: 0x00013304 File Offset: 0x00011504
			public override object[] ToArray()
			{
				return this.m_InnerArrayList.ToArray();
			}

			// Token: 0x0600040D RID: 1037 RVA: 0x00013314 File Offset: 0x00011514
			public override Array ToArray(Type elementType)
			{
				return this.m_InnerArrayList.ToArray(elementType);
			}

			// Token: 0x040001CC RID: 460
			protected ArrayList m_InnerArrayList;
		}

		// Token: 0x02000079 RID: 121
		private sealed class SimpleEnumerator : IEnumerator, ICloneable
		{
			// Token: 0x0600040E RID: 1038 RVA: 0x00013324 File Offset: 0x00011524
			public SimpleEnumerator(ArrayList list)
			{
				this.list = list;
				this.index = -1;
				this.version = list._version;
				this.currentElement = ArrayList.SimpleEnumerator.endFlag;
			}

			// Token: 0x06000410 RID: 1040 RVA: 0x00013360 File Offset: 0x00011560
			public object Clone()
			{
				return base.MemberwiseClone();
			}

			// Token: 0x06000411 RID: 1041 RVA: 0x00013368 File Offset: 0x00011568
			public bool MoveNext()
			{
				if (this.version != this.list._version)
				{
					throw new InvalidOperationException("List has changed.");
				}
				if (++this.index < this.list.Count)
				{
					this.currentElement = this.list[this.index];
					return true;
				}
				this.currentElement = ArrayList.SimpleEnumerator.endFlag;
				return false;
			}

			// Token: 0x17000069 RID: 105
			// (get) Token: 0x06000412 RID: 1042 RVA: 0x000133DC File Offset: 0x000115DC
			public object Current
			{
				get
				{
					if (this.currentElement != ArrayList.SimpleEnumerator.endFlag)
					{
						return this.currentElement;
					}
					if (this.index == -1)
					{
						throw new InvalidOperationException("Enumerator not started");
					}
					throw new InvalidOperationException("Enumerator ended");
				}
			}

			// Token: 0x06000413 RID: 1043 RVA: 0x00013418 File Offset: 0x00011618
			public void Reset()
			{
				if (this.version != this.list._version)
				{
					throw new InvalidOperationException("List has changed.");
				}
				this.currentElement = ArrayList.SimpleEnumerator.endFlag;
				this.index = -1;
			}

			// Token: 0x040001CD RID: 461
			private ArrayList list;

			// Token: 0x040001CE RID: 462
			private int index;

			// Token: 0x040001CF RID: 463
			private int version;

			// Token: 0x040001D0 RID: 464
			private object currentElement;

			// Token: 0x040001D1 RID: 465
			private static object endFlag = new object();
		}

		// Token: 0x0200007A RID: 122
		[Serializable]
		private sealed class SynchronizedArrayListWrapper : ArrayList.ArrayListWrapper
		{
			// Token: 0x06000414 RID: 1044 RVA: 0x00013450 File Offset: 0x00011650
			internal SynchronizedArrayListWrapper(ArrayList innerArrayList) : base(innerArrayList)
			{
				this.m_SyncRoot = innerArrayList.SyncRoot;
			}

			// Token: 0x1700006A RID: 106
			public override object this[int index]
			{
				get
				{
					object syncRoot = this.m_SyncRoot;
					object result;
					lock (syncRoot)
					{
						result = this.m_InnerArrayList[index];
					}
					return result;
				}
				set
				{
					object syncRoot = this.m_SyncRoot;
					lock (syncRoot)
					{
						this.m_InnerArrayList[index] = value;
					}
				}
			}

			// Token: 0x1700006B RID: 107
			// (get) Token: 0x06000417 RID: 1047 RVA: 0x000134F8 File Offset: 0x000116F8
			public override int Count
			{
				get
				{
					object syncRoot = this.m_SyncRoot;
					int count;
					lock (syncRoot)
					{
						count = this.m_InnerArrayList.Count;
					}
					return count;
				}
			}

			// Token: 0x1700006C RID: 108
			// (get) Token: 0x06000418 RID: 1048 RVA: 0x00013540 File Offset: 0x00011740
			// (set) Token: 0x06000419 RID: 1049 RVA: 0x00013588 File Offset: 0x00011788
			public override int Capacity
			{
				get
				{
					object syncRoot = this.m_SyncRoot;
					int capacity;
					lock (syncRoot)
					{
						capacity = this.m_InnerArrayList.Capacity;
					}
					return capacity;
				}
				set
				{
					object syncRoot = this.m_SyncRoot;
					lock (syncRoot)
					{
						this.m_InnerArrayList.Capacity = value;
					}
				}
			}

			// Token: 0x1700006D RID: 109
			// (get) Token: 0x0600041A RID: 1050 RVA: 0x000135CC File Offset: 0x000117CC
			public override bool IsFixedSize
			{
				get
				{
					object syncRoot = this.m_SyncRoot;
					bool isFixedSize;
					lock (syncRoot)
					{
						isFixedSize = this.m_InnerArrayList.IsFixedSize;
					}
					return isFixedSize;
				}
			}

			// Token: 0x1700006E RID: 110
			// (get) Token: 0x0600041B RID: 1051 RVA: 0x00013614 File Offset: 0x00011814
			public override bool IsReadOnly
			{
				get
				{
					object syncRoot = this.m_SyncRoot;
					bool isReadOnly;
					lock (syncRoot)
					{
						isReadOnly = this.m_InnerArrayList.IsReadOnly;
					}
					return isReadOnly;
				}
			}

			// Token: 0x1700006F RID: 111
			// (get) Token: 0x0600041C RID: 1052 RVA: 0x0001365C File Offset: 0x0001185C
			public override bool IsSynchronized
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000070 RID: 112
			// (get) Token: 0x0600041D RID: 1053 RVA: 0x00013660 File Offset: 0x00011860
			public override object SyncRoot
			{
				get
				{
					return this.m_SyncRoot;
				}
			}

			// Token: 0x0600041E RID: 1054 RVA: 0x00013668 File Offset: 0x00011868
			public override int Add(object value)
			{
				object syncRoot = this.m_SyncRoot;
				int result;
				lock (syncRoot)
				{
					result = this.m_InnerArrayList.Add(value);
				}
				return result;
			}

			// Token: 0x0600041F RID: 1055 RVA: 0x000136B4 File Offset: 0x000118B4
			public override void Clear()
			{
				object syncRoot = this.m_SyncRoot;
				lock (syncRoot)
				{
					this.m_InnerArrayList.Clear();
				}
			}

			// Token: 0x06000420 RID: 1056 RVA: 0x000136F8 File Offset: 0x000118F8
			public override bool Contains(object value)
			{
				object syncRoot = this.m_SyncRoot;
				bool result;
				lock (syncRoot)
				{
					result = this.m_InnerArrayList.Contains(value);
				}
				return result;
			}

			// Token: 0x06000421 RID: 1057 RVA: 0x00013744 File Offset: 0x00011944
			public override int IndexOf(object value)
			{
				object syncRoot = this.m_SyncRoot;
				int result;
				lock (syncRoot)
				{
					result = this.m_InnerArrayList.IndexOf(value);
				}
				return result;
			}

			// Token: 0x06000422 RID: 1058 RVA: 0x00013790 File Offset: 0x00011990
			public override int IndexOf(object value, int startIndex)
			{
				object syncRoot = this.m_SyncRoot;
				int result;
				lock (syncRoot)
				{
					result = this.m_InnerArrayList.IndexOf(value, startIndex);
				}
				return result;
			}

			// Token: 0x06000423 RID: 1059 RVA: 0x000137DC File Offset: 0x000119DC
			public override int IndexOf(object value, int startIndex, int count)
			{
				object syncRoot = this.m_SyncRoot;
				int result;
				lock (syncRoot)
				{
					result = this.m_InnerArrayList.IndexOf(value, startIndex, count);
				}
				return result;
			}

			// Token: 0x06000424 RID: 1060 RVA: 0x00013828 File Offset: 0x00011A28
			public override void Insert(int index, object value)
			{
				object syncRoot = this.m_SyncRoot;
				lock (syncRoot)
				{
					this.m_InnerArrayList.Insert(index, value);
				}
			}

			// Token: 0x06000425 RID: 1061 RVA: 0x0001386C File Offset: 0x00011A6C
			public override void InsertRange(int index, ICollection c)
			{
				object syncRoot = this.m_SyncRoot;
				lock (syncRoot)
				{
					this.m_InnerArrayList.InsertRange(index, c);
				}
			}

			// Token: 0x06000426 RID: 1062 RVA: 0x000138B0 File Offset: 0x00011AB0
			public override void Remove(object value)
			{
				object syncRoot = this.m_SyncRoot;
				lock (syncRoot)
				{
					this.m_InnerArrayList.Remove(value);
				}
			}

			// Token: 0x06000427 RID: 1063 RVA: 0x000138F4 File Offset: 0x00011AF4
			public override void RemoveAt(int index)
			{
				object syncRoot = this.m_SyncRoot;
				lock (syncRoot)
				{
					this.m_InnerArrayList.RemoveAt(index);
				}
			}

			// Token: 0x06000428 RID: 1064 RVA: 0x00013938 File Offset: 0x00011B38
			public override void CopyTo(Array array)
			{
				object syncRoot = this.m_SyncRoot;
				lock (syncRoot)
				{
					this.m_InnerArrayList.CopyTo(array);
				}
			}

			// Token: 0x06000429 RID: 1065 RVA: 0x0001397C File Offset: 0x00011B7C
			public override void CopyTo(Array array, int index)
			{
				object syncRoot = this.m_SyncRoot;
				lock (syncRoot)
				{
					this.m_InnerArrayList.CopyTo(array, index);
				}
			}

			// Token: 0x0600042A RID: 1066 RVA: 0x000139C0 File Offset: 0x00011BC0
			public override void CopyTo(int index, Array array, int arrayIndex, int count)
			{
				object syncRoot = this.m_SyncRoot;
				lock (syncRoot)
				{
					this.m_InnerArrayList.CopyTo(index, array, arrayIndex, count);
				}
			}

			// Token: 0x0600042B RID: 1067 RVA: 0x00013A08 File Offset: 0x00011C08
			public override IEnumerator GetEnumerator()
			{
				object syncRoot = this.m_SyncRoot;
				IEnumerator enumerator;
				lock (syncRoot)
				{
					enumerator = this.m_InnerArrayList.GetEnumerator();
				}
				return enumerator;
			}

			// Token: 0x0600042C RID: 1068 RVA: 0x00013A50 File Offset: 0x00011C50
			public override void AddRange(ICollection c)
			{
				object syncRoot = this.m_SyncRoot;
				lock (syncRoot)
				{
					this.m_InnerArrayList.AddRange(c);
				}
			}

			// Token: 0x0600042D RID: 1069 RVA: 0x00013A94 File Offset: 0x00011C94
			public override object Clone()
			{
				object syncRoot = this.m_SyncRoot;
				object result;
				lock (syncRoot)
				{
					result = this.m_InnerArrayList.Clone();
				}
				return result;
			}

			// Token: 0x0600042E RID: 1070 RVA: 0x00013ADC File Offset: 0x00011CDC
			public override void Sort()
			{
				object syncRoot = this.m_SyncRoot;
				lock (syncRoot)
				{
					this.m_InnerArrayList.Sort();
				}
			}

			// Token: 0x0600042F RID: 1071 RVA: 0x00013B20 File Offset: 0x00011D20
			public override void Sort(IComparer comparer)
			{
				object syncRoot = this.m_SyncRoot;
				lock (syncRoot)
				{
					this.m_InnerArrayList.Sort(comparer);
				}
			}

			// Token: 0x06000430 RID: 1072 RVA: 0x00013B64 File Offset: 0x00011D64
			public override object[] ToArray()
			{
				object syncRoot = this.m_SyncRoot;
				object[] result;
				lock (syncRoot)
				{
					result = this.m_InnerArrayList.ToArray();
				}
				return result;
			}

			// Token: 0x06000431 RID: 1073 RVA: 0x00013BAC File Offset: 0x00011DAC
			public override Array ToArray(Type elementType)
			{
				object syncRoot = this.m_SyncRoot;
				Array result;
				lock (syncRoot)
				{
					result = this.m_InnerArrayList.ToArray(elementType);
				}
				return result;
			}

			// Token: 0x040001D2 RID: 466
			private object m_SyncRoot;
		}
	}
}
