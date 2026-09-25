using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x02000062 RID: 98
	[ComVisible(true)]
	[Serializable]
	public abstract class Array : ICollection, IEnumerable, IList, ICloneable
	{
		// Token: 0x0600027A RID: 634 RVA: 0x0000ECB0 File Offset: 0x0000CEB0
		private Array()
		{
		}

		// Token: 0x17000046 RID: 70
		object IList.this[int index]
		{
			get
			{
				if (index >= this.Length)
				{
					throw new IndexOutOfRangeException("index");
				}
				if (this.Rank > 1)
				{
					throw new ArgumentException(Locale.GetText("Only single dimension arrays are supported."));
				}
				return this.GetValueImpl(index);
			}
			set
			{
				if (index >= this.Length)
				{
					throw new IndexOutOfRangeException("index");
				}
				if (this.Rank > 1)
				{
					throw new ArgumentException(Locale.GetText("Only single dimension arrays are supported."));
				}
				this.SetValueImpl(value, index);
			}
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000ED34 File Offset: 0x0000CF34
		int IList.Add(object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000ED3C File Offset: 0x0000CF3C
		void IList.Clear()
		{
			Array.Clear(this, this.GetLowerBound(0), this.Length);
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000ED54 File Offset: 0x0000CF54
		bool IList.Contains(object value)
		{
			if (this.Rank > 1)
			{
				throw new RankException(Locale.GetText("Only single dimension arrays are supported."));
			}
			int length = this.Length;
			for (int i = 0; i < length; i++)
			{
				if (object.Equals(this.GetValueImpl(i), value))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000EDAC File Offset: 0x0000CFAC
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		int IList.IndexOf(object value)
		{
			if (this.Rank > 1)
			{
				throw new RankException(Locale.GetText("Only single dimension arrays are supported."));
			}
			int length = this.Length;
			for (int i = 0; i < length; i++)
			{
				if (object.Equals(this.GetValueImpl(i), value))
				{
					return i + this.GetLowerBound(0);
				}
			}
			return this.GetLowerBound(0) - 1;
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000EE14 File Offset: 0x0000D014
		void IList.Insert(int index, object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000EE1C File Offset: 0x0000D01C
		void IList.Remove(object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000EE24 File Offset: 0x0000D024
		void IList.RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000284 RID: 644 RVA: 0x0000EE2C File Offset: 0x0000D02C
		int ICollection.Count
		{
			get
			{
				return this.Length;
			}
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000EE34 File Offset: 0x0000D034
		internal int InternalArray__ICollection_get_Count()
		{
			return this.Length;
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000EE3C File Offset: 0x0000D03C
		internal bool InternalArray__ICollection_get_IsReadOnly()
		{
			return true;
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000EE40 File Offset: 0x0000D040
		internal IEnumerator<T> InternalArray__IEnumerable_GetEnumerator<T>()
		{
			return new Array.InternalEnumerator<T>(this);
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000EE50 File Offset: 0x0000D050
		internal void InternalArray__ICollection_Clear()
		{
			throw new NotSupportedException("Collection is read-only");
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000EE5C File Offset: 0x0000D05C
		internal void InternalArray__ICollection_Add<T>(T item)
		{
			throw new NotSupportedException("Collection is read-only");
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000EE68 File Offset: 0x0000D068
		internal bool InternalArray__ICollection_Remove<T>(T item)
		{
			throw new NotSupportedException("Collection is read-only");
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000EE74 File Offset: 0x0000D074
		internal bool InternalArray__ICollection_Contains<T>(T item)
		{
			if (this.Rank > 1)
			{
				throw new RankException(Locale.GetText("Only single dimension arrays are supported."));
			}
			int length = this.Length;
			for (int i = 0; i < length; i++)
			{
				T t;
				this.GetGenericValueImpl<T>(i, out t);
				if (item == null)
				{
					return t == null;
				}
				if (item.Equals(t))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000EEF4 File Offset: 0x0000D0F4
		internal void InternalArray__ICollection_CopyTo<T>(T[] array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (this.Rank > 1)
			{
				throw new RankException(Locale.GetText("Only single dimension arrays are supported."));
			}
			if (index + this.GetLength(0) > array.GetLowerBound(0) + array.GetLength(0))
			{
				throw new ArgumentException("Destination array was not long enough. Check destIndex and length, and the array's lower bounds.");
			}
			if (array.Rank > 1)
			{
				throw new RankException(Locale.GetText("Only single dimension arrays are supported."));
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", Locale.GetText("Value has to be >= 0."));
			}
			Array.Copy(this, this.GetLowerBound(0), array, index, this.GetLength(0));
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000EFA4 File Offset: 0x0000D1A4
		internal void InternalArray__Insert<T>(int index, T item)
		{
			throw new NotSupportedException("Collection is read-only");
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000EFB0 File Offset: 0x0000D1B0
		internal void InternalArray__RemoveAt(int index)
		{
			throw new NotSupportedException("Collection is read-only");
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000EFBC File Offset: 0x0000D1BC
		internal int InternalArray__IndexOf<T>(T item)
		{
			if (this.Rank > 1)
			{
				throw new RankException(Locale.GetText("Only single dimension arrays are supported."));
			}
			int length = this.Length;
			int i = 0;
			while (i < length)
			{
				T t;
				this.GetGenericValueImpl<T>(i, out t);
				if (item == null)
				{
					if (t == null)
					{
						return i + this.GetLowerBound(0);
					}
					return this.GetLowerBound(0) - 1;
				}
				else
				{
					if (t.Equals(item))
					{
						return i + this.GetLowerBound(0);
					}
					i++;
				}
			}
			return this.GetLowerBound(0) - 1;
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000F05C File Offset: 0x0000D25C
		internal T InternalArray__get_Item<T>(int index)
		{
			if (index >= this.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			T result;
			this.GetGenericValueImpl<T>(index, out result);
			return result;
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000F08C File Offset: 0x0000D28C
		internal void InternalArray__set_Item<T>(int index, T item)
		{
			if (index >= this.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			object[] array = this as object[];
			if (array != null)
			{
				array[index] = item;
				return;
			}
			this.SetGenericValueImpl<T>(index, ref item);
		}

		// Token: 0x06000292 RID: 658
		[MethodImpl(4096)]
		internal extern void GetGenericValueImpl<T>(int pos, out T value);

		// Token: 0x06000293 RID: 659
		[MethodImpl(4096)]
		internal extern void SetGenericValueImpl<T>(int pos, ref T value);

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000294 RID: 660 RVA: 0x0000F0D0 File Offset: 0x0000D2D0
		public int Length
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				int num = this.GetLength(0);
				for (int i = 1; i < this.Rank; i++)
				{
					num *= this.GetLength(i);
				}
				return num;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000295 RID: 661 RVA: 0x0000F108 File Offset: 0x0000D308
		[ComVisible(false)]
		public long LongLength
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return (long)this.Length;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000296 RID: 662 RVA: 0x0000F114 File Offset: 0x0000D314
		public int Rank
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return this.GetRank();
			}
		}

		// Token: 0x06000297 RID: 663
		[MethodImpl(4096)]
		private extern int GetRank();

		// Token: 0x06000298 RID: 664
		[MethodImpl(4096)]
		public extern int GetLength(int dimension);

		// Token: 0x06000299 RID: 665 RVA: 0x0000F11C File Offset: 0x0000D31C
		[ComVisible(false)]
		public long GetLongLength(int dimension)
		{
			return (long)this.GetLength(dimension);
		}

		// Token: 0x0600029A RID: 666
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(4096)]
		public extern int GetLowerBound(int dimension);

		// Token: 0x0600029B RID: 667
		[MethodImpl(4096)]
		public extern object GetValue(params int[] indices);

		// Token: 0x0600029C RID: 668
		[MethodImpl(4096)]
		public extern void SetValue(object value, params int[] indices);

		// Token: 0x0600029D RID: 669
		[MethodImpl(4096)]
		internal extern object GetValueImpl(int pos);

		// Token: 0x0600029E RID: 670
		[MethodImpl(4096)]
		internal extern void SetValueImpl(object value, int pos);

		// Token: 0x0600029F RID: 671
		[MethodImpl(4096)]
		internal static extern bool FastCopy(Array source, int source_idx, Array dest, int dest_idx, int length);

		// Token: 0x060002A0 RID: 672
		[MethodImpl(4096)]
		internal static extern Array CreateInstanceImpl(Type elementType, int[] lengths, int[] bounds);

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x0000F128 File Offset: 0x0000D328
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x0000F12C File Offset: 0x0000D32C
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x0000F130 File Offset: 0x0000D330
		public bool IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x0000F134 File Offset: 0x0000D334
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000F138 File Offset: 0x0000D338
		public IEnumerator GetEnumerator()
		{
			return new Array.SimpleEnumerator(this);
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000F140 File Offset: 0x0000D340
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public int GetUpperBound(int dimension)
		{
			return this.GetLowerBound(dimension) + this.GetLength(dimension) - 1;
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000F154 File Offset: 0x0000D354
		public object GetValue(int index)
		{
			if (this.Rank != 1)
			{
				throw new ArgumentException(Locale.GetText("Array was not a one-dimensional array."));
			}
			if (index < this.GetLowerBound(0) || index > this.GetUpperBound(0))
			{
				throw new IndexOutOfRangeException(Locale.GetText("Index has to be between upper and lower bound of the array."));
			}
			return this.GetValueImpl(index - this.GetLowerBound(0));
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000F1B8 File Offset: 0x0000D3B8
		public object GetValue(int index1, int index2)
		{
			int[] indices = new int[]
			{
				index1,
				index2
			};
			return this.GetValue(indices);
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000F1DC File Offset: 0x0000D3DC
		public object GetValue(int index1, int index2, int index3)
		{
			int[] indices = new int[]
			{
				index1,
				index2,
				index3
			};
			return this.GetValue(indices);
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000F204 File Offset: 0x0000D404
		[ComVisible(false)]
		public object GetValue(long index)
		{
			if (index < 0L || index > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("index", Locale.GetText("Value must be >= 0 and <= Int32.MaxValue."));
			}
			return this.GetValue((int)index);
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000F238 File Offset: 0x0000D438
		[ComVisible(false)]
		public object GetValue(long index1, long index2)
		{
			if (index1 < 0L || index1 > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("index1", Locale.GetText("Value must be >= 0 and <= Int32.MaxValue."));
			}
			if (index2 < 0L || index2 > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("index2", Locale.GetText("Value must be >= 0 and <= Int32.MaxValue."));
			}
			return this.GetValue((int)index1, (int)index2);
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0000F2A4 File Offset: 0x0000D4A4
		[ComVisible(false)]
		public object GetValue(long index1, long index2, long index3)
		{
			if (index1 < 0L || index1 > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("index1", Locale.GetText("Value must be >= 0 and <= Int32.MaxValue."));
			}
			if (index2 < 0L || index2 > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("index2", Locale.GetText("Value must be >= 0 and <= Int32.MaxValue."));
			}
			if (index3 < 0L || index3 > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("index3", Locale.GetText("Value must be >= 0 and <= Int32.MaxValue."));
			}
			return this.GetValue((int)index1, (int)index2, (int)index3);
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000F338 File Offset: 0x0000D538
		[ComVisible(false)]
		public void SetValue(object value, long index)
		{
			if (index < 0L || index > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("index", Locale.GetText("Value must be >= 0 and <= Int32.MaxValue."));
			}
			this.SetValue(value, (int)index);
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000F36C File Offset: 0x0000D56C
		[ComVisible(false)]
		public void SetValue(object value, long index1, long index2)
		{
			if (index1 < 0L || index1 > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("index1", Locale.GetText("Value must be >= 0 and <= Int32.MaxValue."));
			}
			if (index2 < 0L || index2 > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("index2", Locale.GetText("Value must be >= 0 and <= Int32.MaxValue."));
			}
			int[] indices = new int[]
			{
				(int)index1,
				(int)index2
			};
			this.SetValue(value, indices);
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0000F3E4 File Offset: 0x0000D5E4
		[ComVisible(false)]
		public void SetValue(object value, long index1, long index2, long index3)
		{
			if (index1 < 0L || index1 > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("index1", Locale.GetText("Value must be >= 0 and <= Int32.MaxValue."));
			}
			if (index2 < 0L || index2 > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("index2", Locale.GetText("Value must be >= 0 and <= Int32.MaxValue."));
			}
			if (index3 < 0L || index3 > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("index3", Locale.GetText("Value must be >= 0 and <= Int32.MaxValue."));
			}
			int[] indices = new int[]
			{
				(int)index1,
				(int)index2,
				(int)index3
			};
			this.SetValue(value, indices);
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000F490 File Offset: 0x0000D690
		public void SetValue(object value, int index)
		{
			if (this.Rank != 1)
			{
				throw new ArgumentException(Locale.GetText("Array was not a one-dimensional array."));
			}
			if (index < this.GetLowerBound(0) || index > this.GetUpperBound(0))
			{
				throw new IndexOutOfRangeException(Locale.GetText("Index has to be >= lower bound and <= upper bound of the array."));
			}
			this.SetValueImpl(value, index - this.GetLowerBound(0));
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000F4F4 File Offset: 0x0000D6F4
		public void SetValue(object value, int index1, int index2)
		{
			int[] indices = new int[]
			{
				index1,
				index2
			};
			this.SetValue(value, indices);
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000F518 File Offset: 0x0000D718
		public void SetValue(object value, int index1, int index2, int index3)
		{
			int[] indices = new int[]
			{
				index1,
				index2,
				index3
			};
			this.SetValue(value, indices);
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000F544 File Offset: 0x0000D744
		public static Array CreateInstance(Type elementType, int length)
		{
			int[] lengths = new int[]
			{
				length
			};
			return Array.CreateInstance(elementType, lengths);
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000F564 File Offset: 0x0000D764
		public static Array CreateInstance(Type elementType, int length1, int length2)
		{
			int[] lengths = new int[]
			{
				length1,
				length2
			};
			return Array.CreateInstance(elementType, lengths);
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000F588 File Offset: 0x0000D788
		public static Array CreateInstance(Type elementType, int length1, int length2, int length3)
		{
			int[] lengths = new int[]
			{
				length1,
				length2,
				length3
			};
			return Array.CreateInstance(elementType, lengths);
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000F5B0 File Offset: 0x0000D7B0
		public static Array CreateInstance(Type elementType, params int[] lengths)
		{
			if (elementType == null)
			{
				throw new ArgumentNullException("elementType");
			}
			if (lengths == null)
			{
				throw new ArgumentNullException("lengths");
			}
			if (lengths.Length > 255)
			{
				throw new TypeLoadException();
			}
			int[] bounds = null;
			elementType = elementType.UnderlyingSystemType;
			if (!elementType.IsSystemType)
			{
				throw new ArgumentException("Type must be a type provided by the runtime.", "elementType");
			}
			if (elementType.Equals(typeof(void)))
			{
				throw new NotSupportedException("Array type can not be void");
			}
			if (elementType.ContainsGenericParameters)
			{
				throw new NotSupportedException("Array type can not be an open generic type");
			}
			return Array.CreateInstanceImpl(elementType, lengths, bounds);
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000F658 File Offset: 0x0000D858
		public static Array CreateInstance(Type elementType, int[] lengths, int[] lowerBounds)
		{
			if (elementType == null)
			{
				throw new ArgumentNullException("elementType");
			}
			if (lengths == null)
			{
				throw new ArgumentNullException("lengths");
			}
			if (lowerBounds == null)
			{
				throw new ArgumentNullException("lowerBounds");
			}
			elementType = elementType.UnderlyingSystemType;
			if (!elementType.IsSystemType)
			{
				throw new ArgumentException("Type must be a type provided by the runtime.", "elementType");
			}
			if (elementType.Equals(typeof(void)))
			{
				throw new NotSupportedException("Array type can not be void");
			}
			if (elementType.ContainsGenericParameters)
			{
				throw new NotSupportedException("Array type can not be an open generic type");
			}
			if (lengths.Length < 1)
			{
				throw new ArgumentException(Locale.GetText("Arrays must contain >= 1 elements."));
			}
			if (lengths.Length != lowerBounds.Length)
			{
				throw new ArgumentException(Locale.GetText("Arrays must be of same size."));
			}
			for (int i = 0; i < lowerBounds.Length; i++)
			{
				if (lengths[i] < 0)
				{
					throw new ArgumentOutOfRangeException("lengths", Locale.GetText("Each value has to be >= 0."));
				}
				if ((long)lowerBounds[i] + (long)lengths[i] > 2147483647L)
				{
					throw new ArgumentOutOfRangeException("lengths", Locale.GetText("Length + bound must not exceed Int32.MaxValue."));
				}
			}
			if (lengths.Length > 255)
			{
				throw new TypeLoadException();
			}
			return Array.CreateInstanceImpl(elementType, lengths, lowerBounds);
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000F79C File Offset: 0x0000D99C
		private static int[] GetIntArray(long[] values)
		{
			int num = values.Length;
			int[] array = new int[num];
			for (int i = 0; i < num; i++)
			{
				long num2 = values[i];
				if (num2 < 0L || num2 > 2147483647L)
				{
					throw new ArgumentOutOfRangeException("values", Locale.GetText("Each value has to be >= 0 and <= Int32.MaxValue."));
				}
				array[i] = (int)num2;
			}
			return array;
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000F7FC File Offset: 0x0000D9FC
		public static Array CreateInstance(Type elementType, params long[] lengths)
		{
			if (lengths == null)
			{
				throw new ArgumentNullException("lengths");
			}
			return Array.CreateInstance(elementType, Array.GetIntArray(lengths));
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000F81C File Offset: 0x0000DA1C
		[ComVisible(false)]
		public object GetValue(params long[] indices)
		{
			if (indices == null)
			{
				throw new ArgumentNullException("indices");
			}
			return this.GetValue(Array.GetIntArray(indices));
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000F83C File Offset: 0x0000DA3C
		[ComVisible(false)]
		public void SetValue(object value, params long[] indices)
		{
			if (indices == null)
			{
				throw new ArgumentNullException("indices");
			}
			this.SetValue(value, Array.GetIntArray(indices));
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000F85C File Offset: 0x0000DA5C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static int BinarySearch(Array array, object value)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (value == null)
			{
				return -1;
			}
			if (array.Rank > 1)
			{
				throw new RankException(Locale.GetText("Only single dimension arrays are supported."));
			}
			if (array.Length == 0)
			{
				return -1;
			}
			if (!(value is IComparable))
			{
				throw new ArgumentException(Locale.GetText("value does not support IComparable."));
			}
			return Array.DoBinarySearch(array, array.GetLowerBound(0), array.GetLength(0), value, null);
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000F8DC File Offset: 0x0000DADC
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static int BinarySearch(Array array, object value, IComparer comparer)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank > 1)
			{
				throw new RankException(Locale.GetText("Only single dimension arrays are supported."));
			}
			if (array.Length == 0)
			{
				return -1;
			}
			if (comparer == null && value != null && !(value is IComparable))
			{
				throw new ArgumentException(Locale.GetText("comparer is null and value does not support IComparable."));
			}
			return Array.DoBinarySearch(array, array.GetLowerBound(0), array.GetLength(0), value, comparer);
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000F960 File Offset: 0x0000DB60
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static int BinarySearch(Array array, int index, int length, object value)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank > 1)
			{
				throw new RankException(Locale.GetText("Only single dimension arrays are supported."));
			}
			if (index < array.GetLowerBound(0))
			{
				throw new ArgumentOutOfRangeException("index", Locale.GetText("index is less than the lower bound of array."));
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", Locale.GetText("Value has to be >= 0."));
			}
			if (index > array.GetLowerBound(0) + array.GetLength(0) - length)
			{
				throw new ArgumentException(Locale.GetText("index and length do not specify a valid range in array."));
			}
			if (array.Length == 0)
			{
				return -1;
			}
			if (value != null && !(value is IComparable))
			{
				throw new ArgumentException(Locale.GetText("value does not support IComparable"));
			}
			return Array.DoBinarySearch(array, index, length, value, null);
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000FA38 File Offset: 0x0000DC38
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static int BinarySearch(Array array, int index, int length, object value, IComparer comparer)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank > 1)
			{
				throw new RankException(Locale.GetText("Only single dimension arrays are supported."));
			}
			if (index < array.GetLowerBound(0))
			{
				throw new ArgumentOutOfRangeException("index", Locale.GetText("index is less than the lower bound of array."));
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", Locale.GetText("Value has to be >= 0."));
			}
			if (index > array.GetLowerBound(0) + array.GetLength(0) - length)
			{
				throw new ArgumentException(Locale.GetText("index and length do not specify a valid range in array."));
			}
			if (array.Length == 0)
			{
				return -1;
			}
			if (comparer == null && value != null && !(value is IComparable))
			{
				throw new ArgumentException(Locale.GetText("comparer is null and value does not support IComparable."));
			}
			return Array.DoBinarySearch(array, index, length, value, comparer);
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000FB18 File Offset: 0x0000DD18
		private static int DoBinarySearch(Array array, int index, int length, object value, IComparer comparer)
		{
			if (comparer == null)
			{
				comparer = Comparer.Default;
			}
			int i = index;
			int num = index + length - 1;
			try
			{
				while (i <= num)
				{
					int num2 = i + (num - i) / 2;
					object valueImpl = array.GetValueImpl(num2);
					int num3 = comparer.Compare(valueImpl, value);
					if (num3 == 0)
					{
						return num2;
					}
					if (num3 > 0)
					{
						num = num2 - 1;
					}
					else
					{
						i = num2 + 1;
					}
				}
			}
			catch (Exception innerException)
			{
				throw new InvalidOperationException(Locale.GetText("Comparer threw an exception."), innerException);
			}
			return ~i;
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000FBBC File Offset: 0x0000DDBC
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static void Clear(Array array, int index, int length)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (length < 0)
			{
				throw new IndexOutOfRangeException("length < 0");
			}
			int lowerBound = array.GetLowerBound(0);
			if (index < lowerBound)
			{
				throw new IndexOutOfRangeException("index < lower bound");
			}
			index -= lowerBound;
			if (index > array.Length - length)
			{
				throw new IndexOutOfRangeException("index + length > size");
			}
			Array.ClearInternal(array, index, length);
		}

		// Token: 0x060002C2 RID: 706
		[MethodImpl(4096)]
		private static extern void ClearInternal(Array a, int index, int count);

		// Token: 0x060002C3 RID: 707
		[MethodImpl(4096)]
		public extern object Clone();

		// Token: 0x060002C4 RID: 708 RVA: 0x0000FC2C File Offset: 0x0000DE2C
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		public static void Copy(Array sourceArray, Array destinationArray, int length)
		{
			if (sourceArray == null)
			{
				throw new ArgumentNullException("sourceArray");
			}
			if (destinationArray == null)
			{
				throw new ArgumentNullException("destinationArray");
			}
			Array.Copy(sourceArray, sourceArray.GetLowerBound(0), destinationArray, destinationArray.GetLowerBound(0), length);
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000FC68 File Offset: 0x0000DE68
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		public static void Copy(Array sourceArray, int sourceIndex, Array destinationArray, int destinationIndex, int length)
		{
			if (sourceArray == null)
			{
				throw new ArgumentNullException("sourceArray");
			}
			if (destinationArray == null)
			{
				throw new ArgumentNullException("destinationArray");
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", Locale.GetText("Value has to be >= 0."));
			}
			if (sourceIndex < 0)
			{
				throw new ArgumentOutOfRangeException("sourceIndex", Locale.GetText("Value has to be >= 0."));
			}
			if (destinationIndex < 0)
			{
				throw new ArgumentOutOfRangeException("destinationIndex", Locale.GetText("Value has to be >= 0."));
			}
			if (Array.FastCopy(sourceArray, sourceIndex, destinationArray, destinationIndex, length))
			{
				return;
			}
			int num = sourceIndex - sourceArray.GetLowerBound(0);
			int num2 = destinationIndex - destinationArray.GetLowerBound(0);
			if (num > sourceArray.Length - length)
			{
				throw new ArgumentException("length");
			}
			if (num2 > destinationArray.Length - length)
			{
				string message = "Destination array was not long enough. Check destIndex and length, and the array's lower bounds";
				throw new ArgumentException(message, string.Empty);
			}
			if (sourceArray.Rank != destinationArray.Rank)
			{
				throw new RankException(Locale.GetText("Arrays must be of same size."));
			}
			Type elementType = sourceArray.GetType().GetElementType();
			Type elementType2 = destinationArray.GetType().GetElementType();
			if (!object.ReferenceEquals(sourceArray, destinationArray) || num > num2)
			{
				for (int i = 0; i < length; i++)
				{
					object valueImpl = sourceArray.GetValueImpl(num + i);
					try
					{
						destinationArray.SetValueImpl(valueImpl, num2 + i);
					}
					catch
					{
						if (elementType.Equals(typeof(object)))
						{
							throw new InvalidCastException();
						}
						throw new ArrayTypeMismatchException(string.Format(Locale.GetText("(Types: source={0};  target={1})"), elementType.FullName, elementType2.FullName));
					}
				}
			}
			else
			{
				for (int j = length - 1; j >= 0; j--)
				{
					object valueImpl2 = sourceArray.GetValueImpl(num + j);
					try
					{
						destinationArray.SetValueImpl(valueImpl2, num2 + j);
					}
					catch
					{
						if (elementType.Equals(typeof(object)))
						{
							throw new InvalidCastException();
						}
						throw new ArrayTypeMismatchException(string.Format(Locale.GetText("(Types: source={0};  target={1})"), elementType.FullName, elementType2.FullName));
					}
				}
			}
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000FEAC File Offset: 0x0000E0AC
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		public static void Copy(Array sourceArray, long sourceIndex, Array destinationArray, long destinationIndex, long length)
		{
			if (sourceArray == null)
			{
				throw new ArgumentNullException("sourceArray");
			}
			if (destinationArray == null)
			{
				throw new ArgumentNullException("destinationArray");
			}
			if (sourceIndex < -2147483648L || sourceIndex > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("sourceIndex", Locale.GetText("Must be in the Int32 range."));
			}
			if (destinationIndex < -2147483648L || destinationIndex > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("destinationIndex", Locale.GetText("Must be in the Int32 range."));
			}
			if (length < 0L || length > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("length", Locale.GetText("Value must be >= 0 and <= Int32.MaxValue."));
			}
			Array.Copy(sourceArray, (int)sourceIndex, destinationArray, (int)destinationIndex, (int)length);
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000FF70 File Offset: 0x0000E170
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		public static void Copy(Array sourceArray, Array destinationArray, long length)
		{
			if (length < 0L || length > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("length", Locale.GetText("Value must be >= 0 and <= Int32.MaxValue."));
			}
			Array.Copy(sourceArray, destinationArray, (int)length);
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000FFA4 File Offset: 0x0000E1A4
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static int IndexOf(Array array, object value)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return Array.IndexOf(array, value, 0, array.Length);
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000FFC8 File Offset: 0x0000E1C8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static int IndexOf(Array array, object value, int startIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return Array.IndexOf(array, value, startIndex, array.Length - startIndex);
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000FFEC File Offset: 0x0000E1EC
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static int IndexOf(Array array, object value, int startIndex, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank > 1)
			{
				throw new RankException(Locale.GetText("Only single dimension arrays are supported."));
			}
			if (count < 0 || startIndex < array.GetLowerBound(0) || startIndex - 1 > array.GetUpperBound(0) - count)
			{
				throw new ArgumentOutOfRangeException();
			}
			int num = startIndex + count;
			for (int i = startIndex; i < num; i++)
			{
				if (object.Equals(array.GetValueImpl(i), value))
				{
					return i;
				}
			}
			return array.GetLowerBound(0) - 1;
		}

		// Token: 0x060002CB RID: 715 RVA: 0x00010084 File Offset: 0x0000E284
		public void Initialize()
		{
		}

		// Token: 0x060002CC RID: 716 RVA: 0x00010088 File Offset: 0x0000E288
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static int LastIndexOf(Array array, object value)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Length == 0)
			{
				return array.GetLowerBound(0) - 1;
			}
			return Array.LastIndexOf(array, value, array.Length - 1);
		}

		// Token: 0x060002CD RID: 717 RVA: 0x000100C0 File Offset: 0x0000E2C0
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static int LastIndexOf(Array array, object value, int startIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return Array.LastIndexOf(array, value, startIndex, startIndex - array.GetLowerBound(0) + 1);
		}

		// Token: 0x060002CE RID: 718 RVA: 0x000100E8 File Offset: 0x0000E2E8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static int LastIndexOf(Array array, object value, int startIndex, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank > 1)
			{
				throw new RankException(Locale.GetText("Only single dimension arrays are supported."));
			}
			int lowerBound = array.GetLowerBound(0);
			if (array.Length == 0)
			{
				return lowerBound - 1;
			}
			if (count < 0 || startIndex < lowerBound || startIndex > array.GetUpperBound(0) || startIndex - count + 1 < lowerBound)
			{
				throw new ArgumentOutOfRangeException();
			}
			for (int i = startIndex; i >= startIndex - count + 1; i--)
			{
				if (object.Equals(array.GetValueImpl(i), value))
				{
					return i;
				}
			}
			return lowerBound - 1;
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00010194 File Offset: 0x0000E394
		private static Array.Swapper get_swapper(Array array)
		{
			if (array is int[])
			{
				return new Array.Swapper(array.int_swapper);
			}
			if (array is double[])
			{
				return new Array.Swapper(array.double_swapper);
			}
			if (array is object[])
			{
				return new Array.Swapper(array.obj_swapper);
			}
			return new Array.Swapper(array.slow_swapper);
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x000101F8 File Offset: 0x0000E3F8
		private static Array.Swapper get_swapper<T>(T[] array)
		{
			if (array is int[])
			{
				return new Array.Swapper(array.int_swapper);
			}
			if (array is double[])
			{
				return new Array.Swapper(array.double_swapper);
			}
			return new Array.Swapper(array.slow_swapper);
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x00010238 File Offset: 0x0000E438
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		public static void Reverse(Array array)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			Array.Reverse(array, array.GetLowerBound(0), array.GetLength(0));
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00010260 File Offset: 0x0000E460
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		public static void Reverse(Array array, int index, int length)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank > 1)
			{
				throw new RankException(Locale.GetText("Only single dimension arrays are supported."));
			}
			if (index < array.GetLowerBound(0) || length < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (index > array.GetUpperBound(0) + 1 - length)
			{
				throw new ArgumentException();
			}
			int num = index + length - 1;
			object[] array2 = array as object[];
			if (array2 != null)
			{
				while (index < num)
				{
					object obj = array2[index];
					array2[index] = array2[num];
					array2[num] = obj;
					index++;
					num--;
				}
				return;
			}
			int[] array3 = array as int[];
			if (array3 != null)
			{
				while (index < num)
				{
					int num2 = array3[index];
					array3[index] = array3[num];
					array3[num] = num2;
					index++;
					num--;
				}
				return;
			}
			double[] array4 = array as double[];
			if (array4 != null)
			{
				while (index < num)
				{
					double num3 = array4[index];
					array4[index] = array4[num];
					array4[num] = num3;
					index++;
					num--;
				}
				return;
			}
			Array.Swapper swapper = Array.get_swapper(array);
			while (index < num)
			{
				swapper(index, num);
				index++;
				num--;
			}
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x00010394 File Offset: 0x0000E594
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		public static void Sort(Array array)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			Array.Sort(array, null, array.GetLowerBound(0), array.GetLength(0), null);
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x000103C0 File Offset: 0x0000E5C0
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		public static void Sort(Array keys, Array items)
		{
			if (keys == null)
			{
				throw new ArgumentNullException("keys");
			}
			Array.Sort(keys, items, keys.GetLowerBound(0), keys.GetLength(0), null);
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x000103EC File Offset: 0x0000E5EC
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		public static void Sort(Array array, IComparer comparer)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			Array.Sort(array, null, array.GetLowerBound(0), array.GetLength(0), comparer);
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x00010418 File Offset: 0x0000E618
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		public static void Sort(Array array, int index, int length)
		{
			Array.Sort(array, null, index, length, null);
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x00010424 File Offset: 0x0000E624
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		public static void Sort(Array keys, Array items, IComparer comparer)
		{
			if (keys == null)
			{
				throw new ArgumentNullException("keys");
			}
			Array.Sort(keys, items, keys.GetLowerBound(0), keys.GetLength(0), comparer);
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00010450 File Offset: 0x0000E650
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		public static void Sort(Array keys, Array items, int index, int length)
		{
			Array.Sort(keys, items, index, length, null);
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0001045C File Offset: 0x0000E65C
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		public static void Sort(Array array, int index, int length, IComparer comparer)
		{
			Array.Sort(array, null, index, length, comparer);
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00010468 File Offset: 0x0000E668
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		public static void Sort(Array keys, Array items, int index, int length, IComparer comparer)
		{
			if (keys == null)
			{
				throw new ArgumentNullException("keys");
			}
			if (keys.Rank > 1 || (items != null && items.Rank > 1))
			{
				throw new RankException();
			}
			if (items != null && keys.GetLowerBound(0) != items.GetLowerBound(0))
			{
				throw new ArgumentException();
			}
			if (index < keys.GetLowerBound(0))
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", Locale.GetText("Value has to be >= 0."));
			}
			if (keys.Length - (index + keys.GetLowerBound(0)) < length || (items != null && index > items.Length - length))
			{
				throw new ArgumentException();
			}
			if (length <= 1)
			{
				return;
			}
			if (comparer == null)
			{
				Array.Swapper swap_items;
				if (items == null)
				{
					swap_items = null;
				}
				else
				{
					swap_items = Array.get_swapper(items);
				}
				if (keys is double[])
				{
					Array.combsort(keys as double[], index, length, swap_items);
					return;
				}
				if (keys is int[])
				{
					Array.combsort(keys as int[], index, length, swap_items);
					return;
				}
				if (keys is char[])
				{
					Array.combsort(keys as char[], index, length, swap_items);
					return;
				}
			}
			try
			{
				int high = index + length - 1;
				Array.qsort(keys, items, index, high, comparer);
			}
			catch (Exception innerException)
			{
				throw new InvalidOperationException(Locale.GetText("The comparer threw an exception."), innerException);
			}
		}

		// Token: 0x060002DB RID: 731 RVA: 0x000105E0 File Offset: 0x0000E7E0
		private void int_swapper(int i, int j)
		{
			int[] array = this as int[];
			int num = array[i];
			array[i] = array[j];
			array[j] = num;
		}

		// Token: 0x060002DC RID: 732 RVA: 0x00010604 File Offset: 0x0000E804
		private void obj_swapper(int i, int j)
		{
			object[] array = this as object[];
			object obj = array[i];
			array[i] = array[j];
			array[j] = obj;
		}

		// Token: 0x060002DD RID: 733 RVA: 0x00010628 File Offset: 0x0000E828
		private void slow_swapper(int i, int j)
		{
			object valueImpl = this.GetValueImpl(i);
			this.SetValueImpl(this.GetValue(j), i);
			this.SetValueImpl(valueImpl, j);
		}

		// Token: 0x060002DE RID: 734 RVA: 0x00010654 File Offset: 0x0000E854
		private void double_swapper(int i, int j)
		{
			double[] array = this as double[];
			double num = array[i];
			array[i] = array[j];
			array[j] = num;
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00010678 File Offset: 0x0000E878
		private static int new_gap(int gap)
		{
			gap = gap * 10 / 13;
			if (gap == 9 || gap == 10)
			{
				return 11;
			}
			if (gap < 1)
			{
				return 1;
			}
			return gap;
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x000106A0 File Offset: 0x0000E8A0
		private static void combsort(double[] array, int start, int size, Array.Swapper swap_items)
		{
			int num = size;
			bool flag;
			do
			{
				num = Array.new_gap(num);
				flag = false;
				int num2 = start + size - num;
				for (int i = start; i < num2; i++)
				{
					int num3 = i + num;
					if (array[i] > array[num3])
					{
						double num4 = array[i];
						array[i] = array[num3];
						array[num3] = num4;
						flag = true;
						if (swap_items != null)
						{
							swap_items(i, num3);
						}
					}
				}
			}
			while (num != 1 || flag);
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0001071C File Offset: 0x0000E91C
		private static void combsort(int[] array, int start, int size, Array.Swapper swap_items)
		{
			int num = size;
			bool flag;
			do
			{
				num = Array.new_gap(num);
				flag = false;
				int num2 = start + size - num;
				for (int i = start; i < num2; i++)
				{
					int num3 = i + num;
					if (array[i] > array[num3])
					{
						int num4 = array[i];
						array[i] = array[num3];
						array[num3] = num4;
						flag = true;
						if (swap_items != null)
						{
							swap_items(i, num3);
						}
					}
				}
			}
			while (num != 1 || flag);
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x00010798 File Offset: 0x0000E998
		private static void combsort(char[] array, int start, int size, Array.Swapper swap_items)
		{
			int num = size;
			bool flag;
			do
			{
				num = Array.new_gap(num);
				flag = false;
				int num2 = start + size - num;
				for (int i = start; i < num2; i++)
				{
					int num3 = i + num;
					if (array[i] > array[num3])
					{
						char c = array[i];
						array[i] = array[num3];
						array[num3] = c;
						flag = true;
						if (swap_items != null)
						{
							swap_items(i, num3);
						}
					}
				}
			}
			while (num != 1 || flag);
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x00010814 File Offset: 0x0000EA14
		private static void qsort(Array keys, Array items, int low0, int high0, IComparer comparer)
		{
			if (low0 >= high0)
			{
				return;
			}
			int num = low0;
			int num2 = high0;
			int pos = num + (num2 - num) / 2;
			object valueImpl = keys.GetValueImpl(pos);
			for (;;)
			{
				while (num < high0 && Array.compare(keys.GetValueImpl(num), valueImpl, comparer) < 0)
				{
					num++;
				}
				while (num2 > low0 && Array.compare(valueImpl, keys.GetValueImpl(num2), comparer) < 0)
				{
					num2--;
				}
				if (num > num2)
				{
					break;
				}
				Array.swap(keys, items, num, num2);
				num++;
				num2--;
			}
			if (low0 < num2)
			{
				Array.qsort(keys, items, low0, num2, comparer);
			}
			if (num < high0)
			{
				Array.qsort(keys, items, num, high0, comparer);
			}
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x000108D4 File Offset: 0x0000EAD4
		private static void swap(Array keys, Array items, int i, int j)
		{
			object valueImpl = keys.GetValueImpl(i);
			keys.SetValueImpl(keys.GetValue(j), i);
			keys.SetValueImpl(valueImpl, j);
			if (items != null)
			{
				valueImpl = items.GetValueImpl(i);
				items.SetValueImpl(items.GetValueImpl(j), i);
				items.SetValueImpl(valueImpl, j);
			}
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x00010924 File Offset: 0x0000EB24
		private static int compare(object value1, object value2, IComparer comparer)
		{
			if (value1 == null)
			{
				return (value2 != null) ? -1 : 0;
			}
			if (value2 == null)
			{
				return 1;
			}
			if (comparer == null)
			{
				return ((IComparable)value1).CompareTo(value2);
			}
			return comparer.Compare(value1, value2);
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x00010960 File Offset: 0x0000EB60
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		public static void Sort<T>(T[] array)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			Array.Sort<T, T>(array, null, 0, array.Length, null);
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x00010980 File Offset: 0x0000EB80
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		public static void Sort<TKey, TValue>(TKey[] keys, TValue[] items)
		{
			if (keys == null)
			{
				throw new ArgumentNullException("keys");
			}
			Array.Sort<TKey, TValue>(keys, items, 0, keys.Length, null);
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x000109A0 File Offset: 0x0000EBA0
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		public static void Sort<T>(T[] array, IComparer<T> comparer)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			Array.Sort<T, T>(array, null, 0, array.Length, comparer);
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x000109C0 File Offset: 0x0000EBC0
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		public static void Sort<TKey, TValue>(TKey[] keys, TValue[] items, IComparer<TKey> comparer)
		{
			if (keys == null)
			{
				throw new ArgumentNullException("keys");
			}
			Array.Sort<TKey, TValue>(keys, items, 0, keys.Length, comparer);
		}

		// Token: 0x060002EA RID: 746 RVA: 0x000109E0 File Offset: 0x0000EBE0
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		public static void Sort<T>(T[] array, int index, int length)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			Array.Sort<T, T>(array, null, index, length, null);
		}

		// Token: 0x060002EB RID: 747 RVA: 0x00010A00 File Offset: 0x0000EC00
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		public static void Sort<TKey, TValue>(TKey[] keys, TValue[] items, int index, int length)
		{
			Array.Sort<TKey, TValue>(keys, items, index, length, null);
		}

		// Token: 0x060002EC RID: 748 RVA: 0x00010A0C File Offset: 0x0000EC0C
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		public static void Sort<T>(T[] array, int index, int length, IComparer<T> comparer)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			Array.Sort<T, T>(array, null, index, length, comparer);
		}

		// Token: 0x060002ED RID: 749 RVA: 0x00010A2C File Offset: 0x0000EC2C
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		public static void Sort<TKey, TValue>(TKey[] keys, TValue[] items, int index, int length, IComparer<TKey> comparer)
		{
			if (keys == null)
			{
				throw new ArgumentNullException("keys");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			if (keys.Length - index < length || (items != null && index > items.Length - length))
			{
				throw new ArgumentException();
			}
			if (length <= 1)
			{
				return;
			}
			if (comparer == null)
			{
				Array.Swapper swap_items;
				if (items == null)
				{
					swap_items = null;
				}
				else
				{
					swap_items = Array.get_swapper<TValue>(items);
				}
				if (keys is double[])
				{
					Array.combsort(keys as double[], index, length, swap_items);
					return;
				}
				if (keys is int[])
				{
					Array.combsort(keys as int[], index, length, swap_items);
					return;
				}
				if (keys is char[])
				{
					Array.combsort(keys as char[], index, length, swap_items);
					return;
				}
			}
			try
			{
				int high = index + length - 1;
				Array.qsort<TKey, TValue>(keys, items, index, high, comparer);
			}
			catch (Exception innerException)
			{
				throw new InvalidOperationException(Locale.GetText("The comparer threw an exception."), innerException);
			}
		}

		// Token: 0x060002EE RID: 750 RVA: 0x00010B50 File Offset: 0x0000ED50
		public static void Sort<T>(T[] array, Comparison<T> comparison)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			Array.Sort<T>(array, array.Length, comparison);
		}

		// Token: 0x060002EF RID: 751 RVA: 0x00010B70 File Offset: 0x0000ED70
		internal static void Sort<T>(T[] array, int length, Comparison<T> comparison)
		{
			if (comparison == null)
			{
				throw new ArgumentNullException("comparison");
			}
			if (length <= 1 || array.Length <= 1)
			{
				return;
			}
			try
			{
				int low = 0;
				int high = length - 1;
				Array.qsort<T>(array, low, high, comparison);
			}
			catch (Exception innerException)
			{
				throw new InvalidOperationException(Locale.GetText("Comparison threw an exception."), innerException);
			}
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x00010BDC File Offset: 0x0000EDDC
		private static void qsort<K, V>(K[] keys, V[] items, int low0, int high0, IComparer<K> comparer)
		{
			if (low0 >= high0)
			{
				return;
			}
			int num = low0;
			int num2 = high0;
			int num3 = num + (num2 - num) / 2;
			K k = keys[num3];
			for (;;)
			{
				while (num < high0 && Array.compare<K>(keys[num], k, comparer) < 0)
				{
					num++;
				}
				while (num2 > low0 && Array.compare<K>(k, keys[num2], comparer) < 0)
				{
					num2--;
				}
				if (num > num2)
				{
					break;
				}
				Array.swap<K, V>(keys, items, num, num2);
				num++;
				num2--;
			}
			if (low0 < num2)
			{
				Array.qsort<K, V>(keys, items, low0, num2, comparer);
			}
			if (num < high0)
			{
				Array.qsort<K, V>(keys, items, num, high0, comparer);
			}
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x00010C9C File Offset: 0x0000EE9C
		private static int compare<T>(T value1, T value2, IComparer<T> comparer)
		{
			if (comparer != null)
			{
				return comparer.Compare(value1, value2);
			}
			if (value1 == null)
			{
				return (value2 != null) ? -1 : 0;
			}
			if (value2 == null)
			{
				return 1;
			}
			if (value1 is IComparable<T>)
			{
				return ((IComparable<T>)((object)value1)).CompareTo(value2);
			}
			if (value1 is IComparable)
			{
				return ((IComparable)((object)value1)).CompareTo(value2);
			}
			string text = Locale.GetText("No IComparable or IComparable<{0}> interface found.");
			throw new InvalidOperationException(string.Format(text, typeof(T)));
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x00010D4C File Offset: 0x0000EF4C
		private static void qsort<T>(T[] array, int low0, int high0, Comparison<T> comparison)
		{
			if (low0 >= high0)
			{
				return;
			}
			int num = low0;
			int num2 = high0;
			int num3 = num + (num2 - num) / 2;
			T t = array[num3];
			for (;;)
			{
				while (num < high0 && comparison(array[num], t) < 0)
				{
					num++;
				}
				while (num2 > low0 && comparison(t, array[num2]) < 0)
				{
					num2--;
				}
				if (num > num2)
				{
					break;
				}
				Array.swap<T>(array, num, num2);
				num++;
				num2--;
			}
			if (low0 < num2)
			{
				Array.qsort<T>(array, low0, num2, comparison);
			}
			if (num < high0)
			{
				Array.qsort<T>(array, num, high0, comparison);
			}
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x00010E04 File Offset: 0x0000F004
		private static void swap<K, V>(K[] keys, V[] items, int i, int j)
		{
			K k = keys[i];
			keys[i] = keys[j];
			keys[j] = k;
			if (items != null)
			{
				V v = items[i];
				items[i] = items[j];
				items[j] = v;
			}
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x00010E54 File Offset: 0x0000F054
		private static void swap<T>(T[] array, int i, int j)
		{
			T t = array[i];
			array[i] = array[j];
			array[j] = t;
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x00010E80 File Offset: 0x0000F080
		public void CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (this.Rank > 1)
			{
				throw new RankException(Locale.GetText("Only single dimension arrays are supported."));
			}
			if (index + this.GetLength(0) > array.GetLowerBound(0) + array.GetLength(0))
			{
				throw new ArgumentException("Destination array was not long enough. Check destIndex and length, and the array's lower bounds.");
			}
			if (array.Rank > 1)
			{
				throw new RankException(Locale.GetText("Only single dimension arrays are supported."));
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", Locale.GetText("Value has to be >= 0."));
			}
			Array.Copy(this, this.GetLowerBound(0), array, index, this.GetLength(0));
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x00010F30 File Offset: 0x0000F130
		[ComVisible(false)]
		public void CopyTo(Array array, long index)
		{
			if (index < 0L || index > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("index", Locale.GetText("Value must be >= 0 and <= Int32.MaxValue."));
			}
			this.CopyTo(array, (int)index);
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x00010F64 File Offset: 0x0000F164
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static void Resize<T>(ref T[] array, int newSize)
		{
			Array.Resize<T>(ref array, (array != null) ? array.Length : 0, newSize);
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x00010F80 File Offset: 0x0000F180
		internal static void Resize<T>(ref T[] array, int length, int newSize)
		{
			if (newSize < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (array == null)
			{
				array = new T[newSize];
				return;
			}
			if (array.Length == newSize)
			{
				return;
			}
			T[] array2 = new T[newSize];
			Array.Copy(array, array2, Math.Min(newSize, length));
			array = array2;
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x00010FD0 File Offset: 0x0000F1D0
		public static bool TrueForAll<T>(T[] array, Predicate<T> match)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			foreach (T obj in array)
			{
				if (!match(obj))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0001102C File Offset: 0x0000F22C
		public static void ForEach<T>(T[] array, Action<T> action)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			foreach (T obj in array)
			{
				action(obj);
			}
		}

		// Token: 0x060002FB RID: 763 RVA: 0x00011080 File Offset: 0x0000F280
		public static TOutput[] ConvertAll<TInput, TOutput>(TInput[] array, Converter<TInput, TOutput> converter)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (converter == null)
			{
				throw new ArgumentNullException("converter");
			}
			TOutput[] array2 = new TOutput[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = converter(array[i]);
			}
			return array2;
		}

		// Token: 0x060002FC RID: 764 RVA: 0x000110E4 File Offset: 0x0000F2E4
		public static int FindLastIndex<T>(T[] array, Predicate<T> match)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return Array.FindLastIndex<T>(array, 0, array.Length, match);
		}

		// Token: 0x060002FD RID: 765 RVA: 0x00011104 File Offset: 0x0000F304
		public static int FindLastIndex<T>(T[] array, int startIndex, Predicate<T> match)
		{
			if (array == null)
			{
				throw new ArgumentNullException();
			}
			return Array.FindLastIndex<T>(array, startIndex, array.Length - startIndex, match);
		}

		// Token: 0x060002FE RID: 766 RVA: 0x00011120 File Offset: 0x0000F320
		public static int FindLastIndex<T>(T[] array, int startIndex, int count, Predicate<T> match)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			if (startIndex > array.Length || startIndex + count > array.Length)
			{
				throw new ArgumentOutOfRangeException();
			}
			for (int i = startIndex + count - 1; i >= startIndex; i--)
			{
				if (match(array[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060002FF RID: 767 RVA: 0x00011194 File Offset: 0x0000F394
		public static int FindIndex<T>(T[] array, Predicate<T> match)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return Array.FindIndex<T>(array, 0, array.Length, match);
		}

		// Token: 0x06000300 RID: 768 RVA: 0x000111B4 File Offset: 0x0000F3B4
		public static int FindIndex<T>(T[] array, int startIndex, Predicate<T> match)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return Array.FindIndex<T>(array, startIndex, array.Length - startIndex, match);
		}

		// Token: 0x06000301 RID: 769 RVA: 0x000111D4 File Offset: 0x0000F3D4
		public static int FindIndex<T>(T[] array, int startIndex, int count, Predicate<T> match)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			if (startIndex > array.Length || startIndex + count > array.Length)
			{
				throw new ArgumentOutOfRangeException();
			}
			for (int i = startIndex; i < startIndex + count; i++)
			{
				if (match(array[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000302 RID: 770 RVA: 0x00011248 File Offset: 0x0000F448
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static int BinarySearch<T>(T[] array, T value)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return Array.BinarySearch<T>(array, 0, array.Length, value, null);
		}

		// Token: 0x06000303 RID: 771 RVA: 0x00011268 File Offset: 0x0000F468
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static int BinarySearch<T>(T[] array, T value, IComparer<T> comparer)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return Array.BinarySearch<T>(array, 0, array.Length, value, comparer);
		}

		// Token: 0x06000304 RID: 772 RVA: 0x00011288 File Offset: 0x0000F488
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static int BinarySearch<T>(T[] array, int index, int length, T value)
		{
			return Array.BinarySearch<T>(array, index, length, value, null);
		}

		// Token: 0x06000305 RID: 773 RVA: 0x00011294 File Offset: 0x0000F494
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static int BinarySearch<T>(T[] array, int index, int length, T value, IComparer<T> comparer)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", Locale.GetText("index is less than the lower bound of array."));
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", Locale.GetText("Value has to be >= 0."));
			}
			if (index > array.Length - length)
			{
				throw new ArgumentException(Locale.GetText("index and length do not specify a valid range in array."));
			}
			if (comparer == null)
			{
				comparer = Comparer<T>.Default;
			}
			int i = index;
			int num = index + length - 1;
			try
			{
				while (i <= num)
				{
					int num2 = i + (num - i) / 2;
					int num3 = comparer.Compare(value, array[num2]);
					if (num3 == 0)
					{
						return num2;
					}
					if (num3 < 0)
					{
						num = num2 - 1;
					}
					else
					{
						i = num2 + 1;
					}
				}
			}
			catch (Exception innerException)
			{
				throw new InvalidOperationException(Locale.GetText("Comparer threw an exception."), innerException);
			}
			return ~i;
		}

		// Token: 0x06000306 RID: 774 RVA: 0x00011398 File Offset: 0x0000F598
		public static int IndexOf<T>(T[] array, T value)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return Array.IndexOf<T>(array, value, 0, array.Length);
		}

		// Token: 0x06000307 RID: 775 RVA: 0x000113B8 File Offset: 0x0000F5B8
		public static int IndexOf<T>(T[] array, T value, int startIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return Array.IndexOf<T>(array, value, startIndex, array.Length - startIndex);
		}

		// Token: 0x06000308 RID: 776 RVA: 0x000113D8 File Offset: 0x0000F5D8
		public static int IndexOf<T>(T[] array, T value, int startIndex, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (count < 0 || startIndex < array.GetLowerBound(0) || startIndex - 1 > array.GetUpperBound(0) - count)
			{
				throw new ArgumentOutOfRangeException();
			}
			int num = startIndex + count;
			EqualityComparer<T> @default = EqualityComparer<T>.Default;
			for (int i = startIndex; i < num; i++)
			{
				if (@default.Equals(array[i], value))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000309 RID: 777 RVA: 0x00011454 File Offset: 0x0000F654
		public static int LastIndexOf<T>(T[] array, T value)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Length == 0)
			{
				return -1;
			}
			return Array.LastIndexOf<T>(array, value, array.Length - 1);
		}

		// Token: 0x0600030A RID: 778 RVA: 0x00011480 File Offset: 0x0000F680
		public static int LastIndexOf<T>(T[] array, T value, int startIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return Array.LastIndexOf<T>(array, value, startIndex, startIndex + 1);
		}

		// Token: 0x0600030B RID: 779 RVA: 0x000114A0 File Offset: 0x0000F6A0
		public static int LastIndexOf<T>(T[] array, T value, int startIndex, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (count < 0 || startIndex < array.GetLowerBound(0) || startIndex > array.GetUpperBound(0) || startIndex - count + 1 < array.GetLowerBound(0))
			{
				throw new ArgumentOutOfRangeException();
			}
			EqualityComparer<T> @default = EqualityComparer<T>.Default;
			for (int i = startIndex; i >= startIndex - count + 1; i--)
			{
				if (@default.Equals(array[i], value))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600030C RID: 780 RVA: 0x00011528 File Offset: 0x0000F728
		public static T[] FindAll<T>(T[] array, Predicate<T> match)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			int newSize = 0;
			T[] array2 = new T[array.Length];
			foreach (T t in array)
			{
				if (match(t))
				{
					array2[newSize++] = t;
				}
			}
			Array.Resize<T>(ref array2, newSize);
			return array2;
		}

		// Token: 0x0600030D RID: 781 RVA: 0x000115A8 File Offset: 0x0000F7A8
		public static bool Exists<T>(T[] array, Predicate<T> match)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			foreach (T obj in array)
			{
				if (match(obj))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600030E RID: 782 RVA: 0x00011604 File Offset: 0x0000F804
		public static ReadOnlyCollection<T> AsReadOnly<T>(T[] array)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return new ReadOnlyCollection<T>(new Array.ArrayReadOnlyList<T>(array));
		}

		// Token: 0x0600030F RID: 783 RVA: 0x00011624 File Offset: 0x0000F824
		public static T Find<T>(T[] array, Predicate<T> match)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			foreach (T t in array)
			{
				if (match(t))
				{
					return t;
				}
			}
			return default(T);
		}

		// Token: 0x06000310 RID: 784 RVA: 0x00011688 File Offset: 0x0000F888
		public static T FindLast<T>(T[] array, Predicate<T> match)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			for (int i = array.Length - 1; i >= 0; i--)
			{
				if (match(array[i]))
				{
					return array[i];
				}
			}
			return default(T);
		}

		// Token: 0x06000311 RID: 785 RVA: 0x000116F0 File Offset: 0x0000F8F0
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static void ConstrainedCopy(Array sourceArray, int sourceIndex, Array destinationArray, int destinationIndex, int length)
		{
			Array.Copy(sourceArray, sourceIndex, destinationArray, destinationIndex, length);
		}

		// Token: 0x02000063 RID: 99
		private class ArrayReadOnlyList<T> : ICollection<T>, IEnumerable<T>, IList<T>, IEnumerable
		{
			// Token: 0x06000312 RID: 786 RVA: 0x00011700 File Offset: 0x0000F900
			public ArrayReadOnlyList(T[] array)
			{
				this.array = array;
			}

			// Token: 0x06000313 RID: 787 RVA: 0x00011710 File Offset: 0x0000F910
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x1700004F RID: 79
			public T this[int index]
			{
				get
				{
					if (index >= this.array.Length)
					{
						throw new ArgumentOutOfRangeException("index");
					}
					return this.array[index];
				}
				set
				{
					throw Array.ArrayReadOnlyList<T>.ReadOnlyError();
				}
			}

			// Token: 0x17000050 RID: 80
			// (get) Token: 0x06000316 RID: 790 RVA: 0x00011748 File Offset: 0x0000F948
			public int Count
			{
				get
				{
					return this.array.Length;
				}
			}

			// Token: 0x17000051 RID: 81
			// (get) Token: 0x06000317 RID: 791 RVA: 0x00011754 File Offset: 0x0000F954
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06000318 RID: 792 RVA: 0x00011758 File Offset: 0x0000F958
			public void Add(T item)
			{
				throw Array.ArrayReadOnlyList<T>.ReadOnlyError();
			}

			// Token: 0x06000319 RID: 793 RVA: 0x00011760 File Offset: 0x0000F960
			public void Clear()
			{
				throw Array.ArrayReadOnlyList<T>.ReadOnlyError();
			}

			// Token: 0x0600031A RID: 794 RVA: 0x00011768 File Offset: 0x0000F968
			public bool Contains(T item)
			{
				return Array.IndexOf<T>(this.array, item) >= 0;
			}

			// Token: 0x0600031B RID: 795 RVA: 0x0001177C File Offset: 0x0000F97C
			public void CopyTo(T[] array, int index)
			{
				this.array.CopyTo(array, index);
			}

			// Token: 0x0600031C RID: 796 RVA: 0x0001178C File Offset: 0x0000F98C
			public IEnumerator<T> GetEnumerator()
			{
				for (int i = 0; i < this.array.Length; i++)
				{
					yield return this.array[i];
				}
				yield break;
			}

			// Token: 0x0600031D RID: 797 RVA: 0x000117A8 File Offset: 0x0000F9A8
			public int IndexOf(T item)
			{
				return Array.IndexOf<T>(this.array, item);
			}

			// Token: 0x0600031E RID: 798 RVA: 0x000117B8 File Offset: 0x0000F9B8
			public void Insert(int index, T item)
			{
				throw Array.ArrayReadOnlyList<T>.ReadOnlyError();
			}

			// Token: 0x0600031F RID: 799 RVA: 0x000117C0 File Offset: 0x0000F9C0
			public bool Remove(T item)
			{
				throw Array.ArrayReadOnlyList<T>.ReadOnlyError();
			}

			// Token: 0x06000320 RID: 800 RVA: 0x000117C8 File Offset: 0x0000F9C8
			public void RemoveAt(int index)
			{
				throw Array.ArrayReadOnlyList<T>.ReadOnlyError();
			}

			// Token: 0x06000321 RID: 801 RVA: 0x000117D0 File Offset: 0x0000F9D0
			private static Exception ReadOnlyError()
			{
				return new NotSupportedException("This collection is read-only.");
			}

			// Token: 0x0400018D RID: 397
			private T[] array;
		}

		// Token: 0x02000065 RID: 101
		internal struct InternalEnumerator<T> : IEnumerator<!0>, IEnumerator, IDisposable
		{
			// Token: 0x06000328 RID: 808 RVA: 0x000118A4 File Offset: 0x0000FAA4
			internal InternalEnumerator(Array array)
			{
				this.array = array;
				this.idx = -2;
			}

			// Token: 0x06000329 RID: 809 RVA: 0x000118B8 File Offset: 0x0000FAB8
			void IEnumerator.Reset()
			{
				this.idx = -2;
			}

			// Token: 0x17000054 RID: 84
			// (get) Token: 0x0600032A RID: 810 RVA: 0x000118C4 File Offset: 0x0000FAC4
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0600032B RID: 811 RVA: 0x000118D4 File Offset: 0x0000FAD4
			public void Dispose()
			{
				this.idx = -2;
			}

			// Token: 0x0600032C RID: 812 RVA: 0x000118E0 File Offset: 0x0000FAE0
			public bool MoveNext()
			{
				if (this.idx == -2)
				{
					this.idx = this.array.Length;
				}
				return this.idx != -1 && --this.idx != -1;
			}

			// Token: 0x17000055 RID: 85
			// (get) Token: 0x0600032D RID: 813 RVA: 0x00011934 File Offset: 0x0000FB34
			public T Current
			{
				get
				{
					if (this.idx == -2)
					{
						throw new InvalidOperationException("Enumeration has not started. Call MoveNext");
					}
					if (this.idx == -1)
					{
						throw new InvalidOperationException("Enumeration already finished");
					}
					return this.array.InternalArray__get_Item<T>(this.array.Length - 1 - this.idx);
				}
			}

			// Token: 0x04000192 RID: 402
			private const int NOT_STARTED = -2;

			// Token: 0x04000193 RID: 403
			private const int FINISHED = -1;

			// Token: 0x04000194 RID: 404
			private Array array;

			// Token: 0x04000195 RID: 405
			private int idx;
		}

		// Token: 0x02000066 RID: 102
		internal class SimpleEnumerator : IEnumerator, ICloneable
		{
			// Token: 0x0600032E RID: 814 RVA: 0x00011990 File Offset: 0x0000FB90
			public SimpleEnumerator(Array arrayToEnumerate)
			{
				this.enumeratee = arrayToEnumerate;
				this.currentpos = -1;
				this.length = arrayToEnumerate.Length;
			}

			// Token: 0x17000056 RID: 86
			// (get) Token: 0x0600032F RID: 815 RVA: 0x000119B4 File Offset: 0x0000FBB4
			public object Current
			{
				get
				{
					if (this.currentpos < 0)
					{
						throw new InvalidOperationException(Locale.GetText("Enumeration has not started."));
					}
					if (this.currentpos >= this.length)
					{
						throw new InvalidOperationException(Locale.GetText("Enumeration has already ended"));
					}
					return this.enumeratee.GetValueImpl(this.currentpos);
				}
			}

			// Token: 0x06000330 RID: 816 RVA: 0x00011A10 File Offset: 0x0000FC10
			public bool MoveNext()
			{
				if (this.currentpos < this.length)
				{
					this.currentpos++;
				}
				return this.currentpos < this.length;
			}

			// Token: 0x06000331 RID: 817 RVA: 0x00011A48 File Offset: 0x0000FC48
			public void Reset()
			{
				this.currentpos = -1;
			}

			// Token: 0x06000332 RID: 818 RVA: 0x00011A54 File Offset: 0x0000FC54
			public object Clone()
			{
				return base.MemberwiseClone();
			}

			// Token: 0x04000196 RID: 406
			private Array enumeratee;

			// Token: 0x04000197 RID: 407
			private int currentpos;

			// Token: 0x04000198 RID: 408
			private int length;
		}

		// Token: 0x02000067 RID: 103
		// (Invoke) Token: 0x06000334 RID: 820
		private delegate void Swapper(int i, int j);
	}
}
