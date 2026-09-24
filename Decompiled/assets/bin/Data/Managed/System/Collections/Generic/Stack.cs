using System;
using System.Runtime.InteropServices;

namespace System.Collections.Generic
{
	// Token: 0x02000003 RID: 3
	[ComVisible(false)]
	[Serializable]
	public class Stack<T> : IEnumerable<T>, ICollection, IEnumerable
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x0000205C File Offset: 0x0000025C
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002060 File Offset: 0x00000260
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002064 File Offset: 0x00000264
		void ICollection.CopyTo(Array dest, int idx)
		{
			try
			{
				if (this._array != null)
				{
					this._array.CopyTo(dest, idx);
					Array.Reverse(dest, idx, this._size);
				}
			}
			catch (ArrayTypeMismatchException)
			{
				throw new ArgumentException();
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020B8 File Offset: 0x000002B8
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020C8 File Offset: 0x000002C8
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020D8 File Offset: 0x000002D8
		public T Peek()
		{
			if (this._size == 0)
			{
				throw new InvalidOperationException();
			}
			return this._array[this._size - 1];
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002100 File Offset: 0x00000300
		public T Pop()
		{
			if (this._size == 0)
			{
				throw new InvalidOperationException();
			}
			this._version++;
			T result = this._array[--this._size];
			this._array[this._size] = default(T);
			return result;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002164 File Offset: 0x00000364
		public void Push(T t)
		{
			if (this._array == null || this._size == this._array.Length)
			{
				Array.Resize<T>(ref this._array, (this._size != 0) ? (2 * this._size) : 16);
			}
			this._version++;
			this._array[this._size++] = t;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000021E0 File Offset: 0x000003E0
		public int Count
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021E8 File Offset: 0x000003E8
		public Stack<T>.Enumerator GetEnumerator()
		{
			return new Stack<T>.Enumerator(this);
		}

		// Token: 0x04000001 RID: 1
		private const int INITIAL_SIZE = 16;

		// Token: 0x04000002 RID: 2
		private T[] _array;

		// Token: 0x04000003 RID: 3
		private int _size;

		// Token: 0x04000004 RID: 4
		private int _version;

		// Token: 0x02000004 RID: 4
		[Serializable]
		public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
		{
			// Token: 0x0600000D RID: 13 RVA: 0x000021F0 File Offset: 0x000003F0
			internal Enumerator(Stack<T> t)
			{
				this.parent = t;
				this.idx = -2;
				this._version = t._version;
			}

			// Token: 0x0600000E RID: 14 RVA: 0x00002210 File Offset: 0x00000410
			void IEnumerator.Reset()
			{
				if (this._version != this.parent._version)
				{
					throw new InvalidOperationException();
				}
				this.idx = -2;
			}

			// Token: 0x17000004 RID: 4
			// (get) Token: 0x0600000F RID: 15 RVA: 0x00002238 File Offset: 0x00000438
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000010 RID: 16 RVA: 0x00002248 File Offset: 0x00000448
			public void Dispose()
			{
				this.idx = -2;
			}

			// Token: 0x06000011 RID: 17 RVA: 0x00002254 File Offset: 0x00000454
			public bool MoveNext()
			{
				if (this._version != this.parent._version)
				{
					throw new InvalidOperationException();
				}
				if (this.idx == -2)
				{
					this.idx = this.parent._size;
				}
				return this.idx != -1 && --this.idx != -1;
			}

			// Token: 0x17000005 RID: 5
			// (get) Token: 0x06000012 RID: 18 RVA: 0x000022C4 File Offset: 0x000004C4
			public T Current
			{
				get
				{
					if (this.idx < 0)
					{
						throw new InvalidOperationException();
					}
					return this.parent._array[this.idx];
				}
			}

			// Token: 0x04000005 RID: 5
			private const int NOT_STARTED = -2;

			// Token: 0x04000006 RID: 6
			private const int FINISHED = -1;

			// Token: 0x04000007 RID: 7
			private Stack<T> parent;

			// Token: 0x04000008 RID: 8
			private int idx;

			// Token: 0x04000009 RID: 9
			private int _version;
		}
	}
}
