using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Text
{
	// Token: 0x020003A5 RID: 933
	[ComVisible(true)]
	[MonoTODO("Serialization format not compatible with .NET")]
	[Serializable]
	public sealed class StringBuilder : ISerializable
	{
		// Token: 0x06001BF9 RID: 7161 RVA: 0x00069460 File Offset: 0x00067660
		public StringBuilder(string value, int startIndex, int length, int capacity) : this(value, startIndex, length, capacity, int.MaxValue)
		{
		}

		// Token: 0x06001BFA RID: 7162 RVA: 0x00069474 File Offset: 0x00067674
		private StringBuilder(string value, int startIndex, int length, int capacity, int maxCapacity)
		{
			if (value == null)
			{
				value = string.Empty;
			}
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", startIndex, "StartIndex cannot be less than zero.");
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", length, "Length cannot be less than zero.");
			}
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", capacity, "capacity must be greater than zero.");
			}
			if (maxCapacity < 1)
			{
				throw new ArgumentOutOfRangeException("maxCapacity", "maxCapacity is less than one.");
			}
			if (capacity > maxCapacity)
			{
				throw new ArgumentOutOfRangeException("capacity", "Capacity exceeds maximum capacity.");
			}
			if (startIndex > value.Length - length)
			{
				throw new ArgumentOutOfRangeException("startIndex", startIndex, "StartIndex and length must refer to a location within the string.");
			}
			if (capacity == 0)
			{
				if (maxCapacity > 16)
				{
					capacity = 16;
				}
				else
				{
					this._str = (this._cached_str = string.Empty);
				}
			}
			this._maxCapacity = maxCapacity;
			if (this._str == null)
			{
				this._str = string.InternalAllocateStr((length <= capacity) ? capacity : length);
			}
			if (length > 0)
			{
				string.CharCopy(this._str, 0, value, startIndex, length);
			}
			this._length = length;
		}

		// Token: 0x06001BFB RID: 7163 RVA: 0x000695BC File Offset: 0x000677BC
		public StringBuilder() : this(null)
		{
		}

		// Token: 0x06001BFC RID: 7164 RVA: 0x000695C8 File Offset: 0x000677C8
		public StringBuilder(int capacity) : this(string.Empty, 0, 0, capacity)
		{
		}

		// Token: 0x06001BFD RID: 7165 RVA: 0x000695D8 File Offset: 0x000677D8
		public StringBuilder(string value)
		{
			if (value == null)
			{
				value = string.Empty;
			}
			this._length = value.Length;
			this._str = (this._cached_str = value);
			this._maxCapacity = int.MaxValue;
		}

		// Token: 0x06001BFE RID: 7166 RVA: 0x00069620 File Offset: 0x00067820
		private StringBuilder(SerializationInfo info, StreamingContext context)
		{
			string text = info.GetString("m_StringValue");
			if (text == null)
			{
				text = string.Empty;
			}
			this._length = text.Length;
			this._str = (this._cached_str = text);
			this._maxCapacity = info.GetInt32("m_MaxCapacity");
			if (this._maxCapacity < 0)
			{
				this._maxCapacity = int.MaxValue;
			}
			this.Capacity = info.GetInt32("Capacity");
		}

		// Token: 0x06001BFF RID: 7167 RVA: 0x000696A0 File Offset: 0x000678A0
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("m_MaxCapacity", this._maxCapacity);
			info.AddValue("Capacity", this.Capacity);
			info.AddValue("m_StringValue", this.ToString());
			info.AddValue("m_currentThread", 0);
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06001C00 RID: 7168 RVA: 0x000696EC File Offset: 0x000678EC
		// (set) Token: 0x06001C01 RID: 7169 RVA: 0x00069718 File Offset: 0x00067918
		public int Capacity
		{
			get
			{
				if (this._str.Length == 0)
				{
					return Math.Min(this._maxCapacity, 16);
				}
				return this._str.Length;
			}
			set
			{
				if (value < this._length)
				{
					throw new ArgumentException("Capacity must be larger than length");
				}
				if (value > this._maxCapacity)
				{
					throw new ArgumentOutOfRangeException("value", "Should be less than or equal to MaxCapacity");
				}
				this.InternalEnsureCapacity(value);
			}
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x06001C02 RID: 7170 RVA: 0x00069754 File Offset: 0x00067954
		// (set) Token: 0x06001C03 RID: 7171 RVA: 0x0006975C File Offset: 0x0006795C
		public int Length
		{
			get
			{
				return this._length;
			}
			set
			{
				if (value < 0 || value > this._maxCapacity)
				{
					throw new ArgumentOutOfRangeException();
				}
				if (value == this._length)
				{
					return;
				}
				if (value < this._length)
				{
					this.InternalEnsureCapacity(value);
					this._length = value;
				}
				else
				{
					this.Append('\0', value - this._length);
				}
			}
		}

		// Token: 0x170004EA RID: 1258
		[IndexerName("Chars")]
		public char this[int index]
		{
			get
			{
				if (index >= this._length || index < 0)
				{
					throw new IndexOutOfRangeException();
				}
				return this._str[index];
			}
			set
			{
				if (index >= this._length || index < 0)
				{
					throw new IndexOutOfRangeException();
				}
				if (this._cached_str != null)
				{
					this.InternalEnsureCapacity(this._length);
				}
				this._str.InternalSetChar(index, value);
			}
		}

		// Token: 0x06001C06 RID: 7174 RVA: 0x00069828 File Offset: 0x00067A28
		public override string ToString()
		{
			if (this._length == 0)
			{
				return string.Empty;
			}
			if (this._cached_str != null)
			{
				return this._cached_str;
			}
			if (this._length < this._str.Length >> 1)
			{
				this._cached_str = this._str.SubstringUnchecked(0, this._length);
				return this._cached_str;
			}
			this._cached_str = this._str;
			this._str.InternalSetLength(this._length);
			return this._str;
		}

		// Token: 0x06001C07 RID: 7175 RVA: 0x000698B4 File Offset: 0x00067AB4
		public string ToString(int startIndex, int length)
		{
			if (startIndex < 0 || length < 0 || startIndex > this._length - length)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (startIndex == 0 && length == this._length)
			{
				return this.ToString();
			}
			return this._str.SubstringUnchecked(startIndex, length);
		}

		// Token: 0x06001C08 RID: 7176 RVA: 0x0006990C File Offset: 0x00067B0C
		public StringBuilder Remove(int startIndex, int length)
		{
			if (startIndex < 0 || length < 0 || startIndex > this._length - length)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (this._cached_str != null)
			{
				this.InternalEnsureCapacity(this._length);
			}
			if (this._length - (startIndex + length) > 0)
			{
				string.CharCopy(this._str, startIndex, this._str, startIndex + length, this._length - (startIndex + length));
			}
			this._length -= length;
			return this;
		}

		// Token: 0x06001C09 RID: 7177 RVA: 0x00069990 File Offset: 0x00067B90
		public StringBuilder Replace(string oldValue, string newValue)
		{
			return this.Replace(oldValue, newValue, 0, this._length);
		}

		// Token: 0x06001C0A RID: 7178 RVA: 0x000699A4 File Offset: 0x00067BA4
		public StringBuilder Replace(string oldValue, string newValue, int startIndex, int count)
		{
			if (oldValue == null)
			{
				throw new ArgumentNullException("The old value cannot be null.");
			}
			if (startIndex < 0 || count < 0 || startIndex > this._length - count)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (oldValue.Length == 0)
			{
				throw new ArgumentException("The old value cannot be zero length.");
			}
			string text = this._str.Substring(startIndex, count);
			string text2 = text.Replace(oldValue, newValue);
			if (text2 == text)
			{
				return this;
			}
			this.InternalEnsureCapacity(text2.Length + (this._length - count));
			if (text2.Length < count)
			{
				string.CharCopy(this._str, startIndex + text2.Length, this._str, startIndex + count, this._length - startIndex - count);
			}
			else if (text2.Length > count)
			{
				string.CharCopyReverse(this._str, startIndex + text2.Length, this._str, startIndex + count, this._length - startIndex - count);
			}
			string.CharCopy(this._str, startIndex, text2, 0, text2.Length);
			this._length = text2.Length + (this._length - count);
			return this;
		}

		// Token: 0x06001C0B RID: 7179 RVA: 0x00069AD0 File Offset: 0x00067CD0
		public StringBuilder Append(string value)
		{
			if (value == null)
			{
				return this;
			}
			if (this._length == 0 && value.Length < this._maxCapacity && value.Length > this._str.Length)
			{
				this._length = value.Length;
				this._cached_str = value;
				this._str = value;
				return this;
			}
			int num = this._length + value.Length;
			if (this._cached_str != null || this._str.Length < num)
			{
				this.InternalEnsureCapacity(num);
			}
			string.CharCopy(this._str, this._length, value, 0, value.Length);
			this._length = num;
			return this;
		}

		// Token: 0x06001C0C RID: 7180 RVA: 0x00069B88 File Offset: 0x00067D88
		public StringBuilder Append(int value)
		{
			return this.Append(value.ToString());
		}

		// Token: 0x06001C0D RID: 7181 RVA: 0x00069B98 File Offset: 0x00067D98
		public StringBuilder Append(long value)
		{
			return this.Append(value.ToString());
		}

		// Token: 0x06001C0E RID: 7182 RVA: 0x00069BA8 File Offset: 0x00067DA8
		public StringBuilder Append(object value)
		{
			if (value == null)
			{
				return this;
			}
			return this.Append(value.ToString());
		}

		// Token: 0x06001C0F RID: 7183 RVA: 0x00069BC0 File Offset: 0x00067DC0
		public StringBuilder Append(char value)
		{
			int num = this._length + 1;
			if (this._cached_str != null || this._str.Length < num)
			{
				this.InternalEnsureCapacity(num);
			}
			this._str.InternalSetChar(this._length, value);
			this._length = num;
			return this;
		}

		// Token: 0x06001C10 RID: 7184 RVA: 0x00069C14 File Offset: 0x00067E14
		public StringBuilder Append(char value, int repeatCount)
		{
			if (repeatCount < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			this.InternalEnsureCapacity(this._length + repeatCount);
			for (int i = 0; i < repeatCount; i++)
			{
				this._str.InternalSetChar(this._length++, value);
			}
			return this;
		}

		// Token: 0x06001C11 RID: 7185 RVA: 0x00069C6C File Offset: 0x00067E6C
		public StringBuilder Append(char[] value, int startIndex, int charCount)
		{
			if (value == null)
			{
				if (startIndex != 0 || charCount != 0)
				{
					throw new ArgumentNullException("value");
				}
				return this;
			}
			else
			{
				if (charCount < 0 || startIndex < 0 || startIndex > value.Length - charCount)
				{
					throw new ArgumentOutOfRangeException();
				}
				int num = this._length + charCount;
				this.InternalEnsureCapacity(num);
				string.CharCopy(this._str, this._length, value, startIndex, charCount);
				this._length = num;
				return this;
			}
		}

		// Token: 0x06001C12 RID: 7186 RVA: 0x00069CE4 File Offset: 0x00067EE4
		public StringBuilder Append(string value, int startIndex, int count)
		{
			if (value == null)
			{
				if (startIndex != 0 && count != 0)
				{
					throw new ArgumentNullException("value");
				}
				return this;
			}
			else
			{
				if (count < 0 || startIndex < 0 || startIndex > value.Length - count)
				{
					throw new ArgumentOutOfRangeException();
				}
				int num = this._length + count;
				if (this._cached_str != null || this._str.Length < num)
				{
					this.InternalEnsureCapacity(num);
				}
				string.CharCopy(this._str, this._length, value, startIndex, count);
				this._length = num;
				return this;
			}
		}

		// Token: 0x06001C13 RID: 7187 RVA: 0x00069D7C File Offset: 0x00067F7C
		[ComVisible(false)]
		public StringBuilder AppendLine()
		{
			return this.Append(Environment.NewLine);
		}

		// Token: 0x06001C14 RID: 7188 RVA: 0x00069D8C File Offset: 0x00067F8C
		[ComVisible(false)]
		public StringBuilder AppendLine(string value)
		{
			return this.Append(value).Append(Environment.NewLine);
		}

		// Token: 0x06001C15 RID: 7189 RVA: 0x00069DA0 File Offset: 0x00067FA0
		public StringBuilder AppendFormat(string format, params object[] args)
		{
			return this.AppendFormat(null, format, args);
		}

		// Token: 0x06001C16 RID: 7190 RVA: 0x00069DAC File Offset: 0x00067FAC
		public StringBuilder AppendFormat(IFormatProvider provider, string format, params object[] args)
		{
			string.FormatHelper(this, provider, format, args);
			return this;
		}

		// Token: 0x06001C17 RID: 7191 RVA: 0x00069DBC File Offset: 0x00067FBC
		public StringBuilder AppendFormat(string format, object arg0)
		{
			return this.AppendFormat(null, format, new object[]
			{
				arg0
			});
		}

		// Token: 0x06001C18 RID: 7192 RVA: 0x00069DD0 File Offset: 0x00067FD0
		public StringBuilder AppendFormat(string format, object arg0, object arg1)
		{
			return this.AppendFormat(null, format, new object[]
			{
				arg0,
				arg1
			});
		}

		// Token: 0x06001C19 RID: 7193 RVA: 0x00069DE8 File Offset: 0x00067FE8
		public StringBuilder AppendFormat(string format, object arg0, object arg1, object arg2)
		{
			return this.AppendFormat(null, format, new object[]
			{
				arg0,
				arg1,
				arg2
			});
		}

		// Token: 0x06001C1A RID: 7194 RVA: 0x00069E08 File Offset: 0x00068008
		public StringBuilder Insert(int index, string value)
		{
			if (index > this._length || index < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (value == null || value.Length == 0)
			{
				return this;
			}
			this.InternalEnsureCapacity(this._length + value.Length);
			string.CharCopyReverse(this._str, index + value.Length, this._str, index, this._length - index);
			string.CharCopy(this._str, index, value, 0, value.Length);
			this._length += value.Length;
			return this;
		}

		// Token: 0x06001C1B RID: 7195 RVA: 0x00069EA0 File Offset: 0x000680A0
		public StringBuilder Insert(int index, char value)
		{
			if (index > this._length || index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			this.InternalEnsureCapacity(this._length + 1);
			string.CharCopyReverse(this._str, index + 1, this._str, index, this._length - index);
			this._str.InternalSetChar(index, value);
			this._length++;
			return this;
		}

		// Token: 0x06001C1C RID: 7196 RVA: 0x00069F14 File Offset: 0x00068114
		public StringBuilder Insert(int index, string value, int count)
		{
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (value != null && value != string.Empty)
			{
				for (int i = 0; i < count; i++)
				{
					this.Insert(index, value);
				}
			}
			return this;
		}

		// Token: 0x06001C1D RID: 7197 RVA: 0x00069F60 File Offset: 0x00068160
		private void InternalEnsureCapacity(int size)
		{
			if (size > this._str.Length || this._cached_str == this._str)
			{
				int num = this._str.Length;
				if (size > num)
				{
					if (this._cached_str == this._str && num < 16)
					{
						num = 16;
					}
					num <<= 1;
					if (size > num)
					{
						num = size;
					}
					if (num >= 2147483647 || num < 0)
					{
						num = int.MaxValue;
					}
					if (num > this._maxCapacity && size <= this._maxCapacity)
					{
						num = this._maxCapacity;
					}
					if (num > this._maxCapacity)
					{
						throw new ArgumentOutOfRangeException("size", "capacity was less than the current size.");
					}
				}
				string text = string.InternalAllocateStr(num);
				if (this._length > 0)
				{
					string.CharCopy(text, 0, this._str, 0, this._length);
				}
				this._str = text;
			}
			this._cached_str = null;
		}

		// Token: 0x04000EF3 RID: 3827
		private const int constDefaultCapacity = 16;

		// Token: 0x04000EF4 RID: 3828
		private int _length;

		// Token: 0x04000EF5 RID: 3829
		private string _str;

		// Token: 0x04000EF6 RID: 3830
		private string _cached_str;

		// Token: 0x04000EF7 RID: 3831
		private int _maxCapacity;
	}
}
