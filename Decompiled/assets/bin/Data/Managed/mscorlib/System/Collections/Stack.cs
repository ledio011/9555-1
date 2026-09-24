using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace System.Collections
{
	// Token: 0x020000BA RID: 186
	[ComVisible(true)]
	[DebuggerTypeProxy(typeof(CollectionDebuggerView))]
	[DebuggerDisplay("Count={Count}")]
	[Serializable]
	public class Stack : ICollection, IEnumerable, ICloneable
	{
		// Token: 0x0600067C RID: 1660 RVA: 0x00019840 File Offset: 0x00017A40
		public Stack()
		{
			this.contents = new object[16];
			this.capacity = 16;
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x00019864 File Offset: 0x00017A64
		public Stack(ICollection col) : this((col != null) ? col.Count : 16)
		{
			if (col == null)
			{
				throw new ArgumentNullException("col");
			}
			foreach (object obj in col)
			{
				this.Push(obj);
			}
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x000198E8 File Offset: 0x00017AE8
		public Stack(int initialCapacity)
		{
			if (initialCapacity < 0)
			{
				throw new ArgumentOutOfRangeException("initialCapacity");
			}
			this.capacity = initialCapacity;
			this.contents = new object[this.capacity];
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x00019924 File Offset: 0x00017B24
		private void Resize(int ncapacity)
		{
			ncapacity = Math.Max(ncapacity, 16);
			object[] destinationArray = new object[ncapacity];
			Array.Copy(this.contents, destinationArray, this.count);
			this.capacity = ncapacity;
			this.contents = destinationArray;
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000680 RID: 1664 RVA: 0x00019964 File Offset: 0x00017B64
		public virtual int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000681 RID: 1665 RVA: 0x0001996C File Offset: 0x00017B6C
		public virtual bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000682 RID: 1666 RVA: 0x00019970 File Offset: 0x00017B70
		public virtual object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x00019974 File Offset: 0x00017B74
		public virtual void Clear()
		{
			this.modCount++;
			for (int i = 0; i < this.count; i++)
			{
				this.contents[i] = null;
			}
			this.count = 0;
			this.current = -1;
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x000199C0 File Offset: 0x00017BC0
		public virtual object Clone()
		{
			return new Stack(this.contents)
			{
				current = this.current,
				count = this.count
			};
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x000199F4 File Offset: 0x00017BF4
		public virtual void CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (array.Rank > 1 || (array.Length > 0 && index >= array.Length) || this.count > array.Length - index)
			{
				throw new ArgumentException();
			}
			for (int num = this.current; num != -1; num--)
			{
				array.SetValue(this.contents[num], this.count - (num + 1) + index);
			}
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x00019A94 File Offset: 0x00017C94
		public virtual IEnumerator GetEnumerator()
		{
			return new Stack.Enumerator(this);
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x00019A9C File Offset: 0x00017C9C
		public virtual object Peek()
		{
			if (this.current == -1)
			{
				throw new InvalidOperationException();
			}
			return this.contents[this.current];
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x00019AC0 File Offset: 0x00017CC0
		public virtual object Pop()
		{
			if (this.current == -1)
			{
				throw new InvalidOperationException();
			}
			this.modCount++;
			object result = this.contents[this.current];
			this.contents[this.current] = null;
			this.count--;
			this.current--;
			if (this.count <= this.capacity / 4 && this.count > 16)
			{
				this.Resize(this.capacity / 2);
			}
			return result;
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x00019B54 File Offset: 0x00017D54
		public virtual void Push(object obj)
		{
			this.modCount++;
			if (this.capacity == this.count)
			{
				this.Resize(this.capacity * 2);
			}
			this.count++;
			this.current++;
			this.contents[this.current] = obj;
		}

		// Token: 0x04000255 RID: 597
		private const int default_capacity = 16;

		// Token: 0x04000256 RID: 598
		private object[] contents;

		// Token: 0x04000257 RID: 599
		private int current = -1;

		// Token: 0x04000258 RID: 600
		private int count;

		// Token: 0x04000259 RID: 601
		private int capacity;

		// Token: 0x0400025A RID: 602
		private int modCount;

		// Token: 0x020000BB RID: 187
		private class Enumerator : IEnumerator, ICloneable
		{
			// Token: 0x0600068A RID: 1674 RVA: 0x00019BB8 File Offset: 0x00017DB8
			internal Enumerator(Stack s)
			{
				this.stack = s;
				this.modCount = s.modCount;
				this.current = -2;
			}

			// Token: 0x0600068B RID: 1675 RVA: 0x00019BDC File Offset: 0x00017DDC
			public object Clone()
			{
				return base.MemberwiseClone();
			}

			// Token: 0x17000118 RID: 280
			// (get) Token: 0x0600068C RID: 1676 RVA: 0x00019BE4 File Offset: 0x00017DE4
			public virtual object Current
			{
				get
				{
					if (this.modCount != this.stack.modCount || this.current == -2 || this.current == -1 || this.current > this.stack.count)
					{
						throw new InvalidOperationException();
					}
					return this.stack.contents[this.current];
				}
			}

			// Token: 0x0600068D RID: 1677 RVA: 0x00019C50 File Offset: 0x00017E50
			public virtual bool MoveNext()
			{
				if (this.modCount != this.stack.modCount)
				{
					throw new InvalidOperationException();
				}
				int num = this.current;
				if (num == -2)
				{
					this.current = this.stack.current;
					return this.current != -1;
				}
				if (num != -1)
				{
					this.current--;
					return this.current != -1;
				}
				return false;
			}

			// Token: 0x0600068E RID: 1678 RVA: 0x00019CD0 File Offset: 0x00017ED0
			public virtual void Reset()
			{
				if (this.modCount != this.stack.modCount)
				{
					throw new InvalidOperationException();
				}
				this.current = -2;
			}

			// Token: 0x0400025B RID: 603
			private const int EOF = -1;

			// Token: 0x0400025C RID: 604
			private const int BOF = -2;

			// Token: 0x0400025D RID: 605
			private Stack stack;

			// Token: 0x0400025E RID: 606
			private int modCount;

			// Token: 0x0400025F RID: 607
			private int current;
		}
	}
}
