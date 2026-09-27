using System;
using System.Collections;
using System.Collections.Generic;

namespace UnityEngine.UI.Collections
{
	// Token: 0x0200003D RID: 61
	internal class IndexedSet<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x06000183 RID: 387 RVA: 0x00005EEC File Offset: 0x000040EC
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00005EF4 File Offset: 0x000040F4
		public void Add(T item)
		{
			if (this.m_Dictionary.ContainsKey(item))
			{
				return;
			}
			this.m_List.Add(item);
			this.m_Dictionary.Add(item, this.m_List.Count - 1);
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00005F30 File Offset: 0x00004130
		public bool Remove(T item)
		{
			int index = -1;
			if (!this.m_Dictionary.TryGetValue(item, out index))
			{
				return false;
			}
			this.RemoveAt(index);
			return true;
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00005F5C File Offset: 0x0000415C
		public IEnumerator<T> GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00005F64 File Offset: 0x00004164
		public void Clear()
		{
			this.m_List.Clear();
			this.m_Dictionary.Clear();
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00005F7C File Offset: 0x0000417C
		public bool Contains(T item)
		{
			return this.m_Dictionary.ContainsKey(item);
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00005F8C File Offset: 0x0000418C
		public void CopyTo(T[] array, int arrayIndex)
		{
			this.m_List.CopyTo(array, arrayIndex);
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600018A RID: 394 RVA: 0x00005F9C File Offset: 0x0000419C
		public int Count
		{
			get
			{
				return this.m_List.Count;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00005FAC File Offset: 0x000041AC
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00005FB0 File Offset: 0x000041B0
		public int IndexOf(T item)
		{
			int result = -1;
			this.m_Dictionary.TryGetValue(item, out result);
			return result;
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00005FD0 File Offset: 0x000041D0
		public void Insert(int index, T item)
		{
			throw new NotSupportedException("Random Insertion is semantically invalid, since this structure does not guarantee ordering.");
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00005FDC File Offset: 0x000041DC
		public void RemoveAt(int index)
		{
			T key = this.m_List[index];
			this.m_Dictionary.Remove(key);
			if (index == this.m_List.Count - 1)
			{
				this.m_List.RemoveAt(index);
			}
			else
			{
				int index2 = this.m_List.Count - 1;
				T t = this.m_List[index2];
				this.m_List[index] = t;
				this.m_Dictionary[t] = index;
				this.m_List.RemoveAt(index2);
			}
		}

		// Token: 0x1700005F RID: 95
		public T this[int index]
		{
			get
			{
				return this.m_List[index];
			}
			set
			{
				T key = this.m_List[index];
				this.m_Dictionary.Remove(key);
				this.m_List[index] = value;
				this.m_Dictionary.Add(key, index);
			}
		}

		// Token: 0x06000191 RID: 401 RVA: 0x000060BC File Offset: 0x000042BC
		public void RemoveAll(Predicate<T> match)
		{
			int i = 0;
			while (i < this.m_List.Count)
			{
				T t = this.m_List[i];
				if (match(t))
				{
					this.Remove(t);
				}
				else
				{
					i++;
				}
			}
		}

		// Token: 0x06000192 RID: 402 RVA: 0x0000610C File Offset: 0x0000430C
		public void Sort(Comparison<T> sortLayoutFunction)
		{
			this.m_List.Sort(sortLayoutFunction);
			for (int i = 0; i < this.m_List.Count; i++)
			{
				T key = this.m_List[i];
				this.m_Dictionary[key] = i;
			}
		}

		// Token: 0x040000CE RID: 206
		private readonly List<T> m_List = new List<T>();

		// Token: 0x040000CF RID: 207
		private Dictionary<T, int> m_Dictionary = new Dictionary<T, int>();
	}
}
