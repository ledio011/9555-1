using System;
using System.Collections;
using System.Collections.Generic;
using Boo.Lang.Runtime;

namespace Boo.Lang
{
	// Token: 0x02000005 RID: 5
	[Serializable]
	public class List<T> : ICollection<T>, IList<T>, IEnumerable<T>, IEquatable<List<T>>, ICollection, IEnumerable, IList
	{
		// Token: 0x0600000E RID: 14 RVA: 0x0000216C File Offset: 0x0000036C
		public List()
		{
			this._items = List<T>.EmptyArray;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002190 File Offset: 0x00000390
		void ICollection<!0>.Add(T item)
		{
			this.Push(item);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000219C File Offset: 0x0000039C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<T>)this).GetEnumerator();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000021A4 File Offset: 0x000003A4
		void IList<!0>.Insert(int index, T item)
		{
			this.Insert(index, item);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000021B0 File Offset: 0x000003B0
		void IList<!0>.RemoveAt(int index)
		{
			this.InnerRemoveAt(this.CheckIndex(this.NormalizeIndex(index)));
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000021C8 File Offset: 0x000003C8
		bool ICollection<!0>.Remove(T item)
		{
			return this.InnerRemove(item);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000021D4 File Offset: 0x000003D4
		int IList.Add(object value)
		{
			this.Add((T)((object)value));
			return this.Count - 1;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000021EC File Offset: 0x000003EC
		void IList.Insert(int index, object value)
		{
			this.Insert(index, List<T>.Coerce(value));
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000021FC File Offset: 0x000003FC
		void IList.Remove(object value)
		{
			this.Remove(List<T>.Coerce(value));
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000220C File Offset: 0x0000040C
		int IList.IndexOf(object value)
		{
			return this.IndexOf(List<T>.Coerce(value));
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000221C File Offset: 0x0000041C
		bool IList.Contains(object value)
		{
			return this.Contains(List<T>.Coerce(value));
		}

		// Token: 0x17000003 RID: 3
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this[index] = List<T>.Coerce(value);
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000224C File Offset: 0x0000044C
		void IList.RemoveAt(int index)
		{
			this.RemoveAt(index);
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002258 File Offset: 0x00000458
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000225C File Offset: 0x0000045C
		void ICollection.CopyTo(Array array, int index)
		{
			Array.Copy(this._items, 0, array, index, this._count);
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002274 File Offset: 0x00000474
		public int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000227C File Offset: 0x0000047C
		public IEnumerator<T> GetEnumerator()
		{
			int originalCount = this._count;
			T[] originalItems = this._items;
			for (int i = 0; i < this._count; i++)
			{
				if (originalCount != this._count || originalItems != this._items)
				{
					throw new InvalidOperationException("The list was modified.");
				}
				yield return this._items[i];
			}
			yield break;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002298 File Offset: 0x00000498
		public void CopyTo(T[] target, int index)
		{
			Array.Copy(this._items, 0, target, index, this._count);
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000022 RID: 34 RVA: 0x000022B0 File Offset: 0x000004B0
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000022B4 File Offset: 0x000004B4
		public object SyncRoot
		{
			get
			{
				return this._items;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000024 RID: 36 RVA: 0x000022BC File Offset: 0x000004BC
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000009 RID: 9
		public T this[int index]
		{
			get
			{
				return this._items[this.CheckIndex(this.NormalizeIndex(index))];
			}
			set
			{
				this._items[this.CheckIndex(this.NormalizeIndex(index))] = value;
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000022F8 File Offset: 0x000004F8
		public List<T> Push(T item)
		{
			return this.Add(item);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002304 File Offset: 0x00000504
		public virtual List<T> Add(T item)
		{
			this.EnsureCapacity(this._count + 1);
			this._items[this._count] = item;
			this._count++;
			return this;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002338 File Offset: 0x00000538
		public override string ToString()
		{
			return "[" + this.Join(", ") + "]";
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002354 File Offset: 0x00000554
		public string Join(string separator)
		{
			return Builtins.join(this, separator);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002360 File Offset: 0x00000560
		public override int GetHashCode()
		{
			int num = this._count;
			for (int i = 0; i < this._count; i++)
			{
				T t = this._items[i];
				if (t != null)
				{
					num ^= t.GetHashCode();
				}
			}
			return num;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000023B4 File Offset: 0x000005B4
		public override bool Equals(object other)
		{
			return this == other || this.Equals(other as List<T>);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000023CC File Offset: 0x000005CC
		public bool Equals(List<T> other)
		{
			if (other == null)
			{
				return false;
			}
			if (object.ReferenceEquals(this, other))
			{
				return true;
			}
			if (this._count != other.Count)
			{
				return false;
			}
			for (int i = 0; i < this._count; i++)
			{
				if (!RuntimeServices.EqualityOperator(this._items[i], other[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002444 File Offset: 0x00000644
		public void Clear()
		{
			for (int i = 0; i < this._count; i++)
			{
				this._items[i] = default(T);
			}
			this._count = 0;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002484 File Offset: 0x00000684
		public bool Contains(T item)
		{
			return -1 != this.IndexOf(item);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002494 File Offset: 0x00000694
		public int IndexOf(T item)
		{
			for (int i = 0; i < this._count; i++)
			{
				if (RuntimeServices.EqualityOperator(this._items[i], item))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000024DC File Offset: 0x000006DC
		public List<T> Insert(int index, T item)
		{
			int num = this.NormalizeIndex(index);
			this.EnsureCapacity(Math.Max(this._count, num) + 1);
			if (num < this._count)
			{
				Array.Copy(this._items, num, this._items, num + 1, this._count - num);
			}
			this._items[num] = item;
			this._count++;
			return this;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000254C File Offset: 0x0000074C
		public List<T> Remove(T item)
		{
			this.InnerRemove(item);
			return this;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002558 File Offset: 0x00000758
		public List<T> RemoveAt(int index)
		{
			this.InnerRemoveAt(this.CheckIndex(this.NormalizeIndex(index)));
			return this;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002570 File Offset: 0x00000770
		private void EnsureCapacity(int minCapacity)
		{
			if (minCapacity > this._items.Length)
			{
				T[] array = this.NewArray(minCapacity);
				Array.Copy(this._items, 0, array, 0, this._count);
				this._items = array;
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000025B0 File Offset: 0x000007B0
		private T[] NewArray(int minCapacity)
		{
			int val = Math.Max(1, this._items.Length) * 2;
			return new T[Math.Max(val, minCapacity)];
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000025DC File Offset: 0x000007DC
		private void InnerRemoveAt(int index)
		{
			this._count--;
			this._items[index] = default(T);
			if (index != this._count)
			{
				Array.Copy(this._items, index + 1, this._items, index, this._count - index);
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002638 File Offset: 0x00000838
		private bool InnerRemove(T item)
		{
			int num = this.IndexOf(item);
			if (num != -1)
			{
				this.InnerRemoveAt(num);
				return true;
			}
			return false;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002660 File Offset: 0x00000860
		private int CheckIndex(int index)
		{
			if (index >= this._count)
			{
				throw new IndexOutOfRangeException();
			}
			return index;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002678 File Offset: 0x00000878
		private int NormalizeIndex(int index)
		{
			return (index >= 0) ? index : (index + this._count);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002690 File Offset: 0x00000890
		private static T Coerce(object value)
		{
			if (value is T)
			{
				return (T)((object)value);
			}
			return (T)((object)RuntimeServices.Coerce(value, typeof(T)));
		}

		// Token: 0x04000003 RID: 3
		private static readonly T[] EmptyArray = new T[0];

		// Token: 0x04000004 RID: 4
		protected T[] _items;

		// Token: 0x04000005 RID: 5
		protected int _count;
	}
}
