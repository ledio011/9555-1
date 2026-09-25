using System;
using System.Runtime.InteropServices;

namespace System.Collections
{
	// Token: 0x0200007B RID: 123
	[ComVisible(true)]
	[Serializable]
	public sealed class BitArray : ICollection, IEnumerable, ICloneable
	{
		// Token: 0x06000432 RID: 1074 RVA: 0x00013BF8 File Offset: 0x00011DF8
		public BitArray(BitArray bits)
		{
			if (bits == null)
			{
				throw new ArgumentNullException("bits");
			}
			this.m_length = bits.m_length;
			this.m_array = new int[(this.m_length + 31) / 32];
			if (this.m_array.Length == 1)
			{
				this.m_array[0] = bits.m_array[0];
			}
			else
			{
				Array.Copy(bits.m_array, this.m_array, this.m_array.Length);
			}
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x00013C7C File Offset: 0x00011E7C
		public BitArray(int length)
		{
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			this.m_length = length;
			this.m_array = new int[(this.m_length + 31) / 32];
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x00013CB4 File Offset: 0x00011EB4
		private byte getByte(int byteIndex)
		{
			int num = byteIndex / 4;
			int num2 = byteIndex % 4 * 8;
			int num3 = this.m_array[num] & 255 << num2;
			return (byte)(num3 >> num2 & 255);
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000435 RID: 1077 RVA: 0x00013CEC File Offset: 0x00011EEC
		public int Count
		{
			get
			{
				return this.m_length;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000436 RID: 1078 RVA: 0x00013CF4 File Offset: 0x00011EF4
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000073 RID: 115
		public bool this[int index]
		{
			get
			{
				return this.Get(index);
			}
			set
			{
				this.Set(index, value);
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000439 RID: 1081 RVA: 0x00013D10 File Offset: 0x00011F10
		public int Length
		{
			get
			{
				return this.m_length;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600043A RID: 1082 RVA: 0x00013D18 File Offset: 0x00011F18
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x00013D1C File Offset: 0x00011F1C
		public object Clone()
		{
			return new BitArray(this);
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x00013D24 File Offset: 0x00011F24
		public void CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException("array", "Array rank must be 1");
			}
			if (index >= array.Length && this.m_length > 0)
			{
				throw new ArgumentException("index", "index is greater than array.Length");
			}
			if (array is bool[])
			{
				if (array.Length - index < this.m_length)
				{
					throw new ArgumentException();
				}
				bool[] array2 = (bool[])array;
				for (int i = 0; i < this.m_length; i++)
				{
					array2[index + i] = this[i];
				}
			}
			else if (array is byte[])
			{
				int num = (this.m_length + 7) / 8;
				if (array.Length - index < num)
				{
					throw new ArgumentException();
				}
				byte[] array3 = (byte[])array;
				for (int j = 0; j < num; j++)
				{
					array3[index + j] = this.getByte(j);
				}
			}
			else
			{
				if (!(array is int[]))
				{
					throw new ArgumentException("array", "Unsupported type");
				}
				Array.Copy(this.m_array, 0, array, index, (this.m_length + 31) / 32);
			}
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x00013E80 File Offset: 0x00012080
		public bool Get(int index)
		{
			if (index < 0 || index >= this.m_length)
			{
				throw new ArgumentOutOfRangeException();
			}
			return (this.m_array[index >> 5] & 1 << index) != 0;
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x00013EB8 File Offset: 0x000120B8
		public void Set(int index, bool value)
		{
			if (index < 0 || index >= this.m_length)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (value)
			{
				this.m_array[index >> 5] |= 1 << index;
			}
			else
			{
				this.m_array[index >> 5] &= ~(1 << index);
			}
			this._version++;
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x00013F34 File Offset: 0x00012134
		public IEnumerator GetEnumerator()
		{
			return new BitArray.BitArrayEnumerator(this);
		}

		// Token: 0x040001D3 RID: 467
		private int[] m_array;

		// Token: 0x040001D4 RID: 468
		private int m_length;

		// Token: 0x040001D5 RID: 469
		private int _version;

		// Token: 0x0200007C RID: 124
		[Serializable]
		private class BitArrayEnumerator : IEnumerator, ICloneable
		{
			// Token: 0x06000440 RID: 1088 RVA: 0x00013F3C File Offset: 0x0001213C
			public BitArrayEnumerator(BitArray ba)
			{
				this._index = -1;
				this._bitArray = ba;
				this._version = ba._version;
			}

			// Token: 0x06000441 RID: 1089 RVA: 0x00013F60 File Offset: 0x00012160
			public object Clone()
			{
				return base.MemberwiseClone();
			}

			// Token: 0x17000076 RID: 118
			// (get) Token: 0x06000442 RID: 1090 RVA: 0x00013F68 File Offset: 0x00012168
			public object Current
			{
				get
				{
					if (this._index == -1)
					{
						throw new InvalidOperationException("Enum not started");
					}
					if (this._index >= this._bitArray.Count)
					{
						throw new InvalidOperationException("Enum Ended");
					}
					return this._current;
				}
			}

			// Token: 0x06000443 RID: 1091 RVA: 0x00013FB8 File Offset: 0x000121B8
			public bool MoveNext()
			{
				this.checkVersion();
				if (this._index < this._bitArray.Count - 1)
				{
					this._current = this._bitArray[++this._index];
					return true;
				}
				this._index = this._bitArray.Count;
				return false;
			}

			// Token: 0x06000444 RID: 1092 RVA: 0x0001401C File Offset: 0x0001221C
			public void Reset()
			{
				this.checkVersion();
				this._index = -1;
			}

			// Token: 0x06000445 RID: 1093 RVA: 0x0001402C File Offset: 0x0001222C
			private void checkVersion()
			{
				if (this._version != this._bitArray._version)
				{
					throw new InvalidOperationException();
				}
			}

			// Token: 0x040001D6 RID: 470
			private BitArray _bitArray;

			// Token: 0x040001D7 RID: 471
			private bool _current;

			// Token: 0x040001D8 RID: 472
			private int _index;

			// Token: 0x040001D9 RID: 473
			private int _version;
		}
	}
}
