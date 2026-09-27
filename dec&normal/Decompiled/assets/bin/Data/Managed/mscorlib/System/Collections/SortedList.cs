using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace System.Collections
{
	// Token: 0x020000B4 RID: 180
	[ComVisible(true)]
	[DebuggerDisplay("Count={Count}")]
	[Serializable]
	public class SortedList : ICollection, IDictionary, IEnumerable, ICloneable
	{
		// Token: 0x0600062D RID: 1581 RVA: 0x00018A94 File Offset: 0x00016C94
		public SortedList() : this(null, SortedList.INITIAL_SIZE)
		{
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x00018AA4 File Offset: 0x00016CA4
		public SortedList(IComparer comparer, int capacity)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity");
			}
			if (capacity == 0)
			{
				this.defaultCapacity = 0;
			}
			else
			{
				this.defaultCapacity = SortedList.INITIAL_SIZE;
			}
			this.comparer = comparer;
			this.InitTable(capacity, true);
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x00018AF8 File Offset: 0x00016CF8
		public SortedList(IDictionary d, IComparer comparer)
		{
			if (d == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			this.InitTable(d.Count, true);
			this.comparer = comparer;
			IDictionaryEnumerator enumerator = d.GetEnumerator();
			while (enumerator.MoveNext())
			{
				this.Add(enumerator.Key, enumerator.Value);
			}
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x00018B68 File Offset: 0x00016D68
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new SortedList.Enumerator(this, SortedList.EnumeratorMode.ENTRY_MODE);
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000632 RID: 1586 RVA: 0x00018B74 File Offset: 0x00016D74
		public virtual int Count
		{
			get
			{
				return this.inUse;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000633 RID: 1587 RVA: 0x00018B7C File Offset: 0x00016D7C
		public virtual bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000634 RID: 1588 RVA: 0x00018B80 File Offset: 0x00016D80
		public virtual object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000635 RID: 1589 RVA: 0x00018B84 File Offset: 0x00016D84
		public virtual bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000636 RID: 1590 RVA: 0x00018B88 File Offset: 0x00016D88
		public virtual bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000637 RID: 1591 RVA: 0x00018B8C File Offset: 0x00016D8C
		public virtual ICollection Keys
		{
			get
			{
				return new SortedList.ListKeys(this);
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000638 RID: 1592 RVA: 0x00018B94 File Offset: 0x00016D94
		public virtual ICollection Values
		{
			get
			{
				return new SortedList.ListValues(this);
			}
		}

		// Token: 0x17000103 RID: 259
		public virtual object this[object key]
		{
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException();
				}
				return this.GetImpl(key);
			}
			set
			{
				if (key == null)
				{
					throw new ArgumentNullException();
				}
				if (this.IsReadOnly)
				{
					throw new NotSupportedException("SortedList is Read Only.");
				}
				if (this.Find(key) < 0 && this.IsFixedSize)
				{
					throw new NotSupportedException("Key not found and SortedList is fixed size.");
				}
				this.PutImpl(key, value, true);
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x0600063B RID: 1595 RVA: 0x00018C10 File Offset: 0x00016E10
		// (set) Token: 0x0600063C RID: 1596 RVA: 0x00018C1C File Offset: 0x00016E1C
		public virtual int Capacity
		{
			get
			{
				return this.table.Length;
			}
			set
			{
				int num = this.table.Length;
				if (this.inUse > value)
				{
					throw new ArgumentOutOfRangeException("capacity too small");
				}
				if (value == 0)
				{
					SortedList.Slot[] destinationArray = new SortedList.Slot[this.defaultCapacity];
					Array.Copy(this.table, destinationArray, this.inUse);
					this.table = destinationArray;
				}
				else if (value > this.inUse)
				{
					SortedList.Slot[] destinationArray2 = new SortedList.Slot[value];
					Array.Copy(this.table, destinationArray2, this.inUse);
					this.table = destinationArray2;
				}
				else if (value > num)
				{
					SortedList.Slot[] destinationArray3 = new SortedList.Slot[value];
					Array.Copy(this.table, destinationArray3, num);
					this.table = destinationArray3;
				}
			}
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x00018CCC File Offset: 0x00016ECC
		public virtual void Add(object key, object value)
		{
			this.PutImpl(key, value, false);
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x00018CD8 File Offset: 0x00016ED8
		public virtual void Clear()
		{
			this.defaultCapacity = SortedList.INITIAL_SIZE;
			this.table = new SortedList.Slot[this.defaultCapacity];
			this.inUse = 0;
			this.modificationCount++;
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x00018D0C File Offset: 0x00016F0C
		public virtual bool Contains(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException();
			}
			bool result;
			try
			{
				result = (this.Find(key) >= 0);
			}
			catch (Exception)
			{
				throw new InvalidOperationException();
			}
			return result;
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x00018D5C File Offset: 0x00016F5C
		public virtual IDictionaryEnumerator GetEnumerator()
		{
			return new SortedList.Enumerator(this, SortedList.EnumeratorMode.ENTRY_MODE);
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x00018D68 File Offset: 0x00016F68
		public virtual void Remove(object key)
		{
			int num = this.IndexOfKey(key);
			if (num >= 0)
			{
				this.RemoveAt(num);
			}
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x00018D8C File Offset: 0x00016F8C
		public virtual void CopyTo(Array array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException();
			}
			if (arrayIndex < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (array.Rank > 1)
			{
				throw new ArgumentException("array is multi-dimensional");
			}
			if (arrayIndex >= array.Length)
			{
				throw new ArgumentNullException("arrayIndex is greater than or equal to array.Length");
			}
			if (this.Count > array.Length - arrayIndex)
			{
				throw new ArgumentNullException("Not enough space in array from arrayIndex to end of array");
			}
			IDictionaryEnumerator enumerator = this.GetEnumerator();
			int num = arrayIndex;
			while (enumerator.MoveNext())
			{
				array.SetValue(enumerator.Entry, num++);
			}
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x00018E30 File Offset: 0x00017030
		public virtual object Clone()
		{
			return new SortedList(this, this.comparer)
			{
				modificationCount = this.modificationCount
			};
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x00018E58 File Offset: 0x00017058
		public virtual IList GetValueList()
		{
			return new SortedList.ListValues(this);
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x00018E60 File Offset: 0x00017060
		public virtual void RemoveAt(int index)
		{
			SortedList.Slot[] array = this.table;
			int count = this.Count;
			if (index >= 0 && index < count)
			{
				if (index != count - 1)
				{
					Array.Copy(array, index + 1, array, index, count - 1 - index);
				}
				else
				{
					array[index].key = null;
					array[index].value = null;
				}
				this.inUse--;
				this.modificationCount++;
				return;
			}
			throw new ArgumentOutOfRangeException("index out of range");
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x00018EF0 File Offset: 0x000170F0
		public virtual int IndexOfKey(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException();
			}
			int num = 0;
			try
			{
				num = this.Find(key);
			}
			catch (Exception)
			{
				throw new InvalidOperationException();
			}
			return num | num >> 31;
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x00018F3C File Offset: 0x0001713C
		public virtual int IndexOfValue(object value)
		{
			if (this.inUse == 0)
			{
				return -1;
			}
			for (int i = 0; i < this.inUse; i++)
			{
				SortedList.Slot slot = this.table[i];
				if (object.Equals(value, slot.value))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x00018F94 File Offset: 0x00017194
		public virtual bool ContainsValue(object value)
		{
			return this.IndexOfValue(value) >= 0;
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x00018FA4 File Offset: 0x000171A4
		public virtual object GetByIndex(int index)
		{
			if (index >= 0 && index < this.Count)
			{
				return this.table[index].value;
			}
			throw new ArgumentOutOfRangeException("index out of range");
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x00018FD8 File Offset: 0x000171D8
		public virtual object GetKey(int index)
		{
			if (index >= 0 && index < this.Count)
			{
				return this.table[index].key;
			}
			throw new ArgumentOutOfRangeException("index out of range");
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x0001900C File Offset: 0x0001720C
		private void EnsureCapacity(int n, int free)
		{
			SortedList.Slot[] array = this.table;
			SortedList.Slot[] array2 = null;
			int capacity = this.Capacity;
			bool flag = free >= 0 && free < this.Count;
			if (n > capacity)
			{
				array2 = new SortedList.Slot[n << 1];
			}
			if (array2 != null)
			{
				if (flag)
				{
					if (free > 0)
					{
						Array.Copy(array, 0, array2, 0, free);
					}
					int num = this.Count - free;
					if (num > 0)
					{
						Array.Copy(array, free, array2, free + 1, num);
					}
				}
				else
				{
					Array.Copy(array, array2, this.Count);
				}
				this.table = array2;
			}
			else if (flag)
			{
				Array.Copy(array, free, array, free + 1, this.Count - free);
			}
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x000190C8 File Offset: 0x000172C8
		private void PutImpl(object key, object value, bool overwrite)
		{
			if (key == null)
			{
				throw new ArgumentNullException("null key");
			}
			SortedList.Slot[] array = this.table;
			int num = -1;
			try
			{
				num = this.Find(key);
			}
			catch (Exception)
			{
				throw new InvalidOperationException();
			}
			if (num >= 0)
			{
				if (!overwrite)
				{
					string text = Locale.GetText("Key '{0}' already exists in list.", new object[]
					{
						key
					});
					throw new ArgumentException(text);
				}
				array[num].value = value;
				this.modificationCount++;
				return;
			}
			else
			{
				num = ~num;
				if (num > this.Capacity + 1)
				{
					throw new Exception(string.Concat(new object[]
					{
						"SortedList::internal error (",
						key,
						", ",
						value,
						") at [",
						num,
						"]"
					}));
				}
				this.EnsureCapacity(this.Count + 1, num);
				array = this.table;
				array[num].key = key;
				array[num].value = value;
				this.inUse++;
				this.modificationCount++;
				return;
			}
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x000191FC File Offset: 0x000173FC
		private object GetImpl(object key)
		{
			int num = this.Find(key);
			if (num >= 0)
			{
				return this.table[num].value;
			}
			return null;
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x0001922C File Offset: 0x0001742C
		private void InitTable(int capacity, bool forceSize)
		{
			if (!forceSize && capacity < this.defaultCapacity)
			{
				capacity = this.defaultCapacity;
			}
			this.table = new SortedList.Slot[capacity];
			this.inUse = 0;
			this.modificationCount = 0;
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x00019264 File Offset: 0x00017464
		private void CopyToArray(Array arr, int i, SortedList.EnumeratorMode mode)
		{
			if (arr == null)
			{
				throw new ArgumentNullException("arr");
			}
			if (i < 0 || i + this.Count > arr.Length)
			{
				throw new ArgumentOutOfRangeException("i");
			}
			IEnumerator enumerator = new SortedList.Enumerator(this, mode);
			while (enumerator.MoveNext())
			{
				object value = enumerator.Current;
				arr.SetValue(value, i++);
			}
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x000192D4 File Offset: 0x000174D4
		private int Find(object key)
		{
			SortedList.Slot[] array = this.table;
			int count = this.Count;
			if (count == 0)
			{
				return -1;
			}
			IComparer comparer;
			if (this.comparer == null)
			{
				IComparer @default = Comparer.Default;
				comparer = @default;
			}
			else
			{
				comparer = this.comparer;
			}
			IComparer comparer2 = comparer;
			int i = 0;
			int num = count - 1;
			while (i <= num)
			{
				int num2 = i + num >> 1;
				int num3 = comparer2.Compare(array[num2].key, key);
				if (num3 == 0)
				{
					return num2;
				}
				if (num3 < 0)
				{
					i = num2 + 1;
				}
				else
				{
					num = num2 - 1;
				}
			}
			return ~i;
		}

		// Token: 0x0400023E RID: 574
		private static readonly int INITIAL_SIZE = 16;

		// Token: 0x0400023F RID: 575
		private int inUse;

		// Token: 0x04000240 RID: 576
		private int modificationCount;

		// Token: 0x04000241 RID: 577
		private SortedList.Slot[] table;

		// Token: 0x04000242 RID: 578
		private IComparer comparer;

		// Token: 0x04000243 RID: 579
		private int defaultCapacity;

		// Token: 0x020000B5 RID: 181
		private sealed class Enumerator : IDictionaryEnumerator, IEnumerator, ICloneable
		{
			// Token: 0x06000651 RID: 1617 RVA: 0x00019370 File Offset: 0x00017570
			public Enumerator(SortedList host, SortedList.EnumeratorMode mode)
			{
				this.host = host;
				this.stamp = host.modificationCount;
				this.size = host.Count;
				this.mode = mode;
				this.Reset();
			}

			// Token: 0x06000653 RID: 1619 RVA: 0x000193B0 File Offset: 0x000175B0
			public void Reset()
			{
				if (this.host.modificationCount != this.stamp || this.invalid)
				{
					throw new InvalidOperationException(SortedList.Enumerator.xstr);
				}
				this.pos = -1;
				this.currentKey = null;
				this.currentValue = null;
			}

			// Token: 0x06000654 RID: 1620 RVA: 0x00019400 File Offset: 0x00017600
			public bool MoveNext()
			{
				if (this.host.modificationCount != this.stamp || this.invalid)
				{
					throw new InvalidOperationException(SortedList.Enumerator.xstr);
				}
				SortedList.Slot[] table = this.host.table;
				if (++this.pos < this.size)
				{
					SortedList.Slot slot = table[this.pos];
					this.currentKey = slot.key;
					this.currentValue = slot.value;
					return true;
				}
				this.currentKey = null;
				this.currentValue = null;
				return false;
			}

			// Token: 0x17000105 RID: 261
			// (get) Token: 0x06000655 RID: 1621 RVA: 0x000194A0 File Offset: 0x000176A0
			public DictionaryEntry Entry
			{
				get
				{
					if (this.invalid || this.pos >= this.size || this.pos == -1)
					{
						throw new InvalidOperationException(SortedList.Enumerator.xstr);
					}
					return new DictionaryEntry(this.currentKey, this.currentValue);
				}
			}

			// Token: 0x17000106 RID: 262
			// (get) Token: 0x06000656 RID: 1622 RVA: 0x000194F4 File Offset: 0x000176F4
			public object Key
			{
				get
				{
					if (this.invalid || this.pos >= this.size || this.pos == -1)
					{
						throw new InvalidOperationException(SortedList.Enumerator.xstr);
					}
					return this.currentKey;
				}
			}

			// Token: 0x17000107 RID: 263
			// (get) Token: 0x06000657 RID: 1623 RVA: 0x00019530 File Offset: 0x00017730
			public object Value
			{
				get
				{
					if (this.invalid || this.pos >= this.size || this.pos == -1)
					{
						throw new InvalidOperationException(SortedList.Enumerator.xstr);
					}
					return this.currentValue;
				}
			}

			// Token: 0x17000108 RID: 264
			// (get) Token: 0x06000658 RID: 1624 RVA: 0x0001956C File Offset: 0x0001776C
			public object Current
			{
				get
				{
					if (this.invalid || this.pos >= this.size || this.pos == -1)
					{
						throw new InvalidOperationException(SortedList.Enumerator.xstr);
					}
					switch (this.mode)
					{
					case SortedList.EnumeratorMode.KEY_MODE:
						return this.currentKey;
					case SortedList.EnumeratorMode.VALUE_MODE:
						return this.currentValue;
					case SortedList.EnumeratorMode.ENTRY_MODE:
						return this.Entry;
					default:
						throw new NotSupportedException(this.mode + " is not a supported mode.");
					}
				}
			}

			// Token: 0x06000659 RID: 1625 RVA: 0x00019600 File Offset: 0x00017800
			public object Clone()
			{
				return new SortedList.Enumerator(this.host, this.mode)
				{
					stamp = this.stamp,
					pos = this.pos,
					size = this.size,
					currentKey = this.currentKey,
					currentValue = this.currentValue,
					invalid = this.invalid
				};
			}

			// Token: 0x04000244 RID: 580
			private SortedList host;

			// Token: 0x04000245 RID: 581
			private int stamp;

			// Token: 0x04000246 RID: 582
			private int pos;

			// Token: 0x04000247 RID: 583
			private int size;

			// Token: 0x04000248 RID: 584
			private SortedList.EnumeratorMode mode;

			// Token: 0x04000249 RID: 585
			private object currentKey;

			// Token: 0x0400024A RID: 586
			private object currentValue;

			// Token: 0x0400024B RID: 587
			private bool invalid;

			// Token: 0x0400024C RID: 588
			private static readonly string xstr = "SortedList.Enumerator: snapshot out of sync.";
		}

		// Token: 0x020000B6 RID: 182
		private enum EnumeratorMode
		{
			// Token: 0x0400024E RID: 590
			KEY_MODE,
			// Token: 0x0400024F RID: 591
			VALUE_MODE,
			// Token: 0x04000250 RID: 592
			ENTRY_MODE
		}

		// Token: 0x020000B7 RID: 183
		[Serializable]
		private class ListKeys : ICollection, IEnumerable, IList
		{
			// Token: 0x0600065A RID: 1626 RVA: 0x00019668 File Offset: 0x00017868
			public ListKeys(SortedList host)
			{
				if (host == null)
				{
					throw new ArgumentNullException();
				}
				this.host = host;
			}

			// Token: 0x17000109 RID: 265
			// (get) Token: 0x0600065B RID: 1627 RVA: 0x00019684 File Offset: 0x00017884
			public virtual int Count
			{
				get
				{
					return this.host.Count;
				}
			}

			// Token: 0x1700010A RID: 266
			// (get) Token: 0x0600065C RID: 1628 RVA: 0x00019694 File Offset: 0x00017894
			public virtual bool IsSynchronized
			{
				get
				{
					return this.host.IsSynchronized;
				}
			}

			// Token: 0x1700010B RID: 267
			// (get) Token: 0x0600065D RID: 1629 RVA: 0x000196A4 File Offset: 0x000178A4
			public virtual object SyncRoot
			{
				get
				{
					return this.host.SyncRoot;
				}
			}

			// Token: 0x0600065E RID: 1630 RVA: 0x000196B4 File Offset: 0x000178B4
			public virtual void CopyTo(Array array, int arrayIndex)
			{
				this.host.CopyToArray(array, arrayIndex, SortedList.EnumeratorMode.KEY_MODE);
			}

			// Token: 0x1700010C RID: 268
			// (get) Token: 0x0600065F RID: 1631 RVA: 0x000196C4 File Offset: 0x000178C4
			public virtual bool IsFixedSize
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700010D RID: 269
			// (get) Token: 0x06000660 RID: 1632 RVA: 0x000196C8 File Offset: 0x000178C8
			public virtual bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700010E RID: 270
			public virtual object this[int index]
			{
				get
				{
					return this.host.GetKey(index);
				}
				set
				{
					throw new NotSupportedException("attempt to modify a key");
				}
			}

			// Token: 0x06000663 RID: 1635 RVA: 0x000196E8 File Offset: 0x000178E8
			public virtual int Add(object value)
			{
				throw new NotSupportedException("IList::Add not supported");
			}

			// Token: 0x06000664 RID: 1636 RVA: 0x000196F4 File Offset: 0x000178F4
			public virtual void Clear()
			{
				throw new NotSupportedException("IList::Clear not supported");
			}

			// Token: 0x06000665 RID: 1637 RVA: 0x00019700 File Offset: 0x00017900
			public virtual bool Contains(object key)
			{
				return this.host.Contains(key);
			}

			// Token: 0x06000666 RID: 1638 RVA: 0x00019710 File Offset: 0x00017910
			public virtual int IndexOf(object key)
			{
				return this.host.IndexOfKey(key);
			}

			// Token: 0x06000667 RID: 1639 RVA: 0x00019720 File Offset: 0x00017920
			public virtual void Insert(int index, object value)
			{
				throw new NotSupportedException("IList::Insert not supported");
			}

			// Token: 0x06000668 RID: 1640 RVA: 0x0001972C File Offset: 0x0001792C
			public virtual void Remove(object value)
			{
				throw new NotSupportedException("IList::Remove not supported");
			}

			// Token: 0x06000669 RID: 1641 RVA: 0x00019738 File Offset: 0x00017938
			public virtual void RemoveAt(int index)
			{
				throw new NotSupportedException("IList::RemoveAt not supported");
			}

			// Token: 0x0600066A RID: 1642 RVA: 0x00019744 File Offset: 0x00017944
			public virtual IEnumerator GetEnumerator()
			{
				return new SortedList.Enumerator(this.host, SortedList.EnumeratorMode.KEY_MODE);
			}

			// Token: 0x04000251 RID: 593
			private SortedList host;
		}

		// Token: 0x020000B8 RID: 184
		[Serializable]
		private class ListValues : ICollection, IEnumerable, IList
		{
			// Token: 0x0600066B RID: 1643 RVA: 0x00019754 File Offset: 0x00017954
			public ListValues(SortedList host)
			{
				if (host == null)
				{
					throw new ArgumentNullException();
				}
				this.host = host;
			}

			// Token: 0x1700010F RID: 271
			// (get) Token: 0x0600066C RID: 1644 RVA: 0x00019770 File Offset: 0x00017970
			public virtual int Count
			{
				get
				{
					return this.host.Count;
				}
			}

			// Token: 0x17000110 RID: 272
			// (get) Token: 0x0600066D RID: 1645 RVA: 0x00019780 File Offset: 0x00017980
			public virtual bool IsSynchronized
			{
				get
				{
					return this.host.IsSynchronized;
				}
			}

			// Token: 0x17000111 RID: 273
			// (get) Token: 0x0600066E RID: 1646 RVA: 0x00019790 File Offset: 0x00017990
			public virtual object SyncRoot
			{
				get
				{
					return this.host.SyncRoot;
				}
			}

			// Token: 0x0600066F RID: 1647 RVA: 0x000197A0 File Offset: 0x000179A0
			public virtual void CopyTo(Array array, int arrayIndex)
			{
				this.host.CopyToArray(array, arrayIndex, SortedList.EnumeratorMode.VALUE_MODE);
			}

			// Token: 0x17000112 RID: 274
			// (get) Token: 0x06000670 RID: 1648 RVA: 0x000197B0 File Offset: 0x000179B0
			public virtual bool IsFixedSize
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000113 RID: 275
			// (get) Token: 0x06000671 RID: 1649 RVA: 0x000197B4 File Offset: 0x000179B4
			public virtual bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000114 RID: 276
			public virtual object this[int index]
			{
				get
				{
					return this.host.GetByIndex(index);
				}
				set
				{
					throw new NotSupportedException("This operation is not supported on GetValueList return");
				}
			}

			// Token: 0x06000674 RID: 1652 RVA: 0x000197D4 File Offset: 0x000179D4
			public virtual int Add(object value)
			{
				throw new NotSupportedException("IList::Add not supported");
			}

			// Token: 0x06000675 RID: 1653 RVA: 0x000197E0 File Offset: 0x000179E0
			public virtual void Clear()
			{
				throw new NotSupportedException("IList::Clear not supported");
			}

			// Token: 0x06000676 RID: 1654 RVA: 0x000197EC File Offset: 0x000179EC
			public virtual bool Contains(object value)
			{
				return this.host.ContainsValue(value);
			}

			// Token: 0x06000677 RID: 1655 RVA: 0x000197FC File Offset: 0x000179FC
			public virtual int IndexOf(object value)
			{
				return this.host.IndexOfValue(value);
			}

			// Token: 0x06000678 RID: 1656 RVA: 0x0001980C File Offset: 0x00017A0C
			public virtual void Insert(int index, object value)
			{
				throw new NotSupportedException("IList::Insert not supported");
			}

			// Token: 0x06000679 RID: 1657 RVA: 0x00019818 File Offset: 0x00017A18
			public virtual void Remove(object value)
			{
				throw new NotSupportedException("IList::Remove not supported");
			}

			// Token: 0x0600067A RID: 1658 RVA: 0x00019824 File Offset: 0x00017A24
			public virtual void RemoveAt(int index)
			{
				throw new NotSupportedException("IList::RemoveAt not supported");
			}

			// Token: 0x0600067B RID: 1659 RVA: 0x00019830 File Offset: 0x00017A30
			public virtual IEnumerator GetEnumerator()
			{
				return new SortedList.Enumerator(this.host, SortedList.EnumeratorMode.VALUE_MODE);
			}

			// Token: 0x04000252 RID: 594
			private SortedList host;
		}

		// Token: 0x020000B9 RID: 185
		[Serializable]
		internal struct Slot
		{
			// Token: 0x04000253 RID: 595
			internal object key;

			// Token: 0x04000254 RID: 596
			internal object value;
		}
	}
}
