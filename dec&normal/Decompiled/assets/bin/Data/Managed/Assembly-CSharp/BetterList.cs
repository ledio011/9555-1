using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

// Token: 0x02000083 RID: 131
public class BetterList<T>
{
	// Token: 0x060002A4 RID: 676 RVA: 0x00012944 File Offset: 0x00010B44
	public IEnumerator<T> GetEnumerator()
	{
		if (this.buffer != null)
		{
			for (int i = 0; i < this.size; i++)
			{
				yield return this.buffer[i];
			}
		}
		yield break;
	}

	// Token: 0x17000052 RID: 82
	[DebuggerHidden]
	public T this[int i]
	{
		get
		{
			return this.buffer[i];
		}
		set
		{
			this.buffer[i] = value;
		}
	}

	// Token: 0x060002A7 RID: 679 RVA: 0x00012980 File Offset: 0x00010B80
	private void AllocateMore()
	{
		T[] array = (this.buffer == null) ? new T[32] : new T[Mathf.Max(this.buffer.Length << 1, 32)];
		if (this.buffer != null && this.size > 0)
		{
			this.buffer.CopyTo(array, 0);
		}
		this.buffer = array;
	}

	// Token: 0x060002A8 RID: 680 RVA: 0x000129E8 File Offset: 0x00010BE8
	private void Trim()
	{
		if (this.size > 0)
		{
			if (this.size < this.buffer.Length)
			{
				T[] array = new T[this.size];
				for (int i = 0; i < this.size; i++)
				{
					array[i] = this.buffer[i];
				}
				this.buffer = array;
			}
		}
		else
		{
			this.buffer = null;
		}
	}

	// Token: 0x060002A9 RID: 681 RVA: 0x00012A60 File Offset: 0x00010C60
	public void Clear()
	{
		this.size = 0;
	}

	// Token: 0x060002AA RID: 682 RVA: 0x00012A6C File Offset: 0x00010C6C
	public void Release()
	{
		this.size = 0;
		this.buffer = null;
	}

	// Token: 0x060002AB RID: 683 RVA: 0x00012A7C File Offset: 0x00010C7C
	public void Add(T item)
	{
		if (this.buffer == null || this.size == this.buffer.Length)
		{
			this.AllocateMore();
		}
		this.buffer[this.size++] = item;
	}

	// Token: 0x060002AC RID: 684 RVA: 0x00012ACC File Offset: 0x00010CCC
	public void Insert(int index, T item)
	{
		if (this.buffer == null || this.size == this.buffer.Length)
		{
			this.AllocateMore();
		}
		if (index < this.size)
		{
			for (int i = this.size; i > index; i--)
			{
				this.buffer[i] = this.buffer[i - 1];
			}
			this.buffer[index] = item;
			this.size++;
		}
		else
		{
			this.Add(item);
		}
	}

	// Token: 0x060002AD RID: 685 RVA: 0x00012B64 File Offset: 0x00010D64
	public bool Contains(T item)
	{
		if (this.buffer == null)
		{
			return false;
		}
		for (int i = 0; i < this.size; i++)
		{
			if (this.buffer[i].Equals(item))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060002AE RID: 686 RVA: 0x00012BBC File Offset: 0x00010DBC
	public int IndexOf(T item)
	{
		if (this.buffer == null)
		{
			return -1;
		}
		for (int i = 0; i < this.size; i++)
		{
			if (this.buffer[i].Equals(item))
			{
				return i;
			}
		}
		return -1;
	}

	// Token: 0x060002AF RID: 687 RVA: 0x00012C14 File Offset: 0x00010E14
	public bool Remove(T item)
	{
		if (this.buffer != null)
		{
			EqualityComparer<T> @default = EqualityComparer<T>.Default;
			for (int i = 0; i < this.size; i++)
			{
				if (@default.Equals(this.buffer[i], item))
				{
					this.size--;
					this.buffer[i] = default(T);
					for (int j = i; j < this.size; j++)
					{
						this.buffer[j] = this.buffer[j + 1];
					}
					this.buffer[this.size] = default(T);
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060002B0 RID: 688 RVA: 0x00012CD4 File Offset: 0x00010ED4
	public void RemoveAt(int index)
	{
		if (this.buffer != null && index < this.size)
		{
			this.size--;
			this.buffer[index] = default(T);
			for (int i = index; i < this.size; i++)
			{
				this.buffer[i] = this.buffer[i + 1];
			}
			this.buffer[this.size] = default(T);
		}
	}

	// Token: 0x060002B1 RID: 689 RVA: 0x00012D68 File Offset: 0x00010F68
	public T Pop()
	{
		if (this.buffer != null && this.size != 0)
		{
			T result = this.buffer[--this.size];
			this.buffer[this.size] = default(T);
			return result;
		}
		return default(T);
	}

	// Token: 0x060002B2 RID: 690 RVA: 0x00012DD0 File Offset: 0x00010FD0
	public T[] ToArray()
	{
		this.Trim();
		return this.buffer;
	}

	// Token: 0x060002B3 RID: 691 RVA: 0x00012DE0 File Offset: 0x00010FE0
	[DebuggerHidden]
	[DebuggerStepThrough]
	public void Sort(BetterList<T>.CompareFunc comparer)
	{
		int num = 0;
		int num2 = this.size - 1;
		bool flag = true;
		while (flag)
		{
			flag = false;
			for (int i = num; i < num2; i++)
			{
				if (comparer(this.buffer[i], this.buffer[i + 1]) > 0)
				{
					T t = this.buffer[i];
					this.buffer[i] = this.buffer[i + 1];
					this.buffer[i + 1] = t;
					flag = true;
				}
				else if (!flag)
				{
					num = ((i != 0) ? (i - 1) : 0);
				}
			}
		}
	}

	// Token: 0x04000304 RID: 772
	public T[] buffer;

	// Token: 0x04000305 RID: 773
	public int size;

	// Token: 0x02000A9A RID: 2714
	// (Invoke) Token: 0x06004EF1 RID: 20209
	public delegate int CompareFunc(T left, T right);
}
