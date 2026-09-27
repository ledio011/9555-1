using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace System.Collections
{
	// Token: 0x020000B2 RID: 178
	[ComVisible(true)]
	[DebuggerTypeProxy(typeof(CollectionDebuggerView))]
	[DebuggerDisplay("Count={Count}")]
	[Serializable]
	public class Queue : ICollection, IEnumerable, ICloneable
	{
		// Token: 0x06000619 RID: 1561 RVA: 0x00018534 File Offset: 0x00016734
		public Queue() : this(32, 2f)
		{
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x00018544 File Offset: 0x00016744
		public Queue(int capacity) : this(capacity, 2f)
		{
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x00018554 File Offset: 0x00016754
		public Queue(ICollection col) : this((col != null) ? col.Count : 32)
		{
			if (col == null)
			{
				throw new ArgumentNullException("col");
			}
			foreach (object obj in col)
			{
				this.Enqueue(obj);
			}
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x000185D8 File Offset: 0x000167D8
		public Queue(int capacity, float growFactor)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", "Needs a non-negative number");
			}
			if (growFactor < 1f || growFactor > 10f)
			{
				throw new ArgumentOutOfRangeException("growFactor", "Queue growth factor must be between 1.0 and 10.0, inclusive");
			}
			this._array = new object[capacity];
			this._growFactor = (int)(growFactor * 100f);
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x0600061D RID: 1565 RVA: 0x00018644 File Offset: 0x00016844
		public virtual int Count
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x0600061E RID: 1566 RVA: 0x0001864C File Offset: 0x0001684C
		public virtual bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600061F RID: 1567 RVA: 0x00018650 File Offset: 0x00016850
		public virtual object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x00018654 File Offset: 0x00016854
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
			if (array.Rank > 1 || (index != 0 && index >= array.Length) || this._size > array.Length - index)
			{
				throw new ArgumentException();
			}
			int num = this._array.Length;
			int num2 = num - this._head;
			Array.Copy(this._array, this._head, array, index, Math.Min(this._size, num2));
			if (this._size > num2)
			{
				Array.Copy(this._array, 0, array, index + num2, this._size - num2);
			}
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x00018710 File Offset: 0x00016910
		public virtual IEnumerator GetEnumerator()
		{
			return new Queue.QueueEnumerator(this);
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x00018718 File Offset: 0x00016918
		public virtual object Clone()
		{
			Queue queue = new Queue(this._array.Length);
			queue._growFactor = this._growFactor;
			Array.Copy(this._array, 0, queue._array, 0, this._array.Length);
			queue._head = this._head;
			queue._size = this._size;
			queue._tail = this._tail;
			return queue;
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x00018780 File Offset: 0x00016980
		public virtual void Clear()
		{
			this._version++;
			this._head = 0;
			this._size = 0;
			this._tail = 0;
			for (int i = this._array.Length - 1; i >= 0; i--)
			{
				this._array[i] = null;
			}
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x000187D4 File Offset: 0x000169D4
		public virtual object Dequeue()
		{
			this._version++;
			if (this._size < 1)
			{
				throw new InvalidOperationException();
			}
			object result = this._array[this._head];
			this._array[this._head] = null;
			this._head = (this._head + 1) % this._array.Length;
			this._size--;
			return result;
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x00018844 File Offset: 0x00016A44
		public virtual void Enqueue(object obj)
		{
			this._version++;
			if (this._size == this._array.Length)
			{
				this.grow();
			}
			this._array[this._tail] = obj;
			this._tail = (this._tail + 1) % this._array.Length;
			this._size++;
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x000188AC File Offset: 0x00016AAC
		public virtual object Peek()
		{
			if (this._size < 1)
			{
				throw new InvalidOperationException();
			}
			return this._array[this._head];
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x000188D0 File Offset: 0x00016AD0
		private void grow()
		{
			int num = this._array.Length * this._growFactor / 100;
			if (num < this._array.Length + 1)
			{
				num = this._array.Length + 1;
			}
			object[] array = new object[num];
			this.CopyTo(array, 0);
			this._array = array;
			this._head = 0;
			this._tail = this._head + this._size;
		}

		// Token: 0x04000235 RID: 565
		private object[] _array;

		// Token: 0x04000236 RID: 566
		private int _head;

		// Token: 0x04000237 RID: 567
		private int _size;

		// Token: 0x04000238 RID: 568
		private int _tail;

		// Token: 0x04000239 RID: 569
		private int _growFactor;

		// Token: 0x0400023A RID: 570
		private int _version;

		// Token: 0x020000B3 RID: 179
		[Serializable]
		private class QueueEnumerator : IEnumerator, ICloneable
		{
			// Token: 0x06000628 RID: 1576 RVA: 0x0001893C File Offset: 0x00016B3C
			internal QueueEnumerator(Queue q)
			{
				this.queue = q;
				this._version = q._version;
				this.current = -1;
			}

			// Token: 0x06000629 RID: 1577 RVA: 0x00018960 File Offset: 0x00016B60
			public object Clone()
			{
				return new Queue.QueueEnumerator(this.queue)
				{
					_version = this._version,
					current = this.current
				};
			}

			// Token: 0x170000FB RID: 251
			// (get) Token: 0x0600062A RID: 1578 RVA: 0x00018994 File Offset: 0x00016B94
			public virtual object Current
			{
				get
				{
					if (this._version != this.queue._version || this.current < 0 || this.current >= this.queue._size)
					{
						throw new InvalidOperationException();
					}
					return this.queue._array[(this.queue._head + this.current) % this.queue._array.Length];
				}
			}

			// Token: 0x0600062B RID: 1579 RVA: 0x00018A0C File Offset: 0x00016C0C
			public virtual bool MoveNext()
			{
				if (this._version != this.queue._version)
				{
					throw new InvalidOperationException();
				}
				if (this.current >= this.queue._size - 1)
				{
					this.current = int.MaxValue;
					return false;
				}
				this.current++;
				return true;
			}

			// Token: 0x0600062C RID: 1580 RVA: 0x00018A6C File Offset: 0x00016C6C
			public virtual void Reset()
			{
				if (this._version != this.queue._version)
				{
					throw new InvalidOperationException();
				}
				this.current = -1;
			}

			// Token: 0x0400023B RID: 571
			private Queue queue;

			// Token: 0x0400023C RID: 572
			private int _version;

			// Token: 0x0400023D RID: 573
			private int current;
		}
	}
}
