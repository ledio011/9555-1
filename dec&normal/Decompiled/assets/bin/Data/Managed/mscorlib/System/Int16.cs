using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x02000113 RID: 275
	[ComVisible(true)]
	[Serializable]
	public struct Int16 : IComparable<short>, IEquatable<short>, IComparable, IConvertible, IFormattable
	{
		// Token: 0x06000A9C RID: 2716 RVA: 0x00028794 File Offset: 0x00026994
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this);
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x000287A0 File Offset: 0x000269A0
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x000287AC File Offset: 0x000269AC
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return Convert.ToChar(this);
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x000287B8 File Offset: 0x000269B8
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			return Convert.ToDateTime(this);
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x000287C4 File Offset: 0x000269C4
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this);
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x000287D0 File Offset: 0x000269D0
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this);
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x000287DC File Offset: 0x000269DC
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x000287E8 File Offset: 0x000269E8
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this);
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x000287F4 File Offset: 0x000269F4
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x00028800 File Offset: 0x00026A00
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x0002880C File Offset: 0x00026A0C
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this);
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x00028818 File Offset: 0x00026A18
		object IConvertible.ToType(Type targetType, IFormatProvider provider)
		{
			if (targetType == null)
			{
				throw new ArgumentNullException("targetType");
			}
			return Convert.ToType(this, targetType, provider, false);
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x0002883C File Offset: 0x00026A3C
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x00028848 File Offset: 0x00026A48
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x00028854 File Offset: 0x00026A54
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x00028860 File Offset: 0x00026A60
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is short))
			{
				throw new ArgumentException(Locale.GetText("Value is not a System.Int16"));
			}
			short num = (short)value;
			if (this == num)
			{
				return 0;
			}
			if (this > num)
			{
				return 1;
			}
			return -1;
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x000288AC File Offset: 0x00026AAC
		public override bool Equals(object obj)
		{
			return obj is short && (short)obj == this;
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x000288C8 File Offset: 0x00026AC8
		public override int GetHashCode()
		{
			return (int)this;
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x000288CC File Offset: 0x00026ACC
		public int CompareTo(short value)
		{
			if (this == value)
			{
				return 0;
			}
			if (this > value)
			{
				return 1;
			}
			return -1;
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x000288E4 File Offset: 0x00026AE4
		public bool Equals(short obj)
		{
			return obj == this;
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x000288EC File Offset: 0x00026AEC
		internal static bool Parse(string s, bool tryParse, out short result, out Exception exc)
		{
			short num = 0;
			int num2 = 1;
			bool flag = false;
			result = 0;
			exc = null;
			if (s == null)
			{
				if (!tryParse)
				{
					exc = new ArgumentNullException("s");
				}
				return false;
			}
			int length = s.Length;
			int i;
			char c;
			for (i = 0; i < length; i++)
			{
				c = s[i];
				if (!char.IsWhiteSpace(c))
				{
					break;
				}
			}
			if (i == length)
			{
				if (!tryParse)
				{
					exc = int.GetFormatException();
				}
				return false;
			}
			c = s[i];
			if (c == '+')
			{
				i++;
			}
			else if (c == '-')
			{
				num2 = -1;
				i++;
			}
			while (i < length)
			{
				c = s[i];
				if (c >= '0' && c <= '9')
				{
					byte b = (byte)(c - '0');
					if (num <= 3276)
					{
						if (num != 3276)
						{
							num = num * 10 + (short)b;
							flag = true;
							goto IL_154;
						}
						if (b <= 7 || (num2 != 1 && b <= 8))
						{
							if (num2 == -1)
							{
								num = (short)((int)num * num2 * 10 - (int)b);
							}
							else
							{
								num = num * 10 + (short)b;
							}
							if (int.ProcessTrailingWhitespace(tryParse, s, i + 1, ref exc))
							{
								result = num;
								return true;
							}
						}
					}
					if (!tryParse)
					{
						exc = new OverflowException("Value is too large");
					}
					return false;
				}
				if (!int.ProcessTrailingWhitespace(tryParse, s, i, ref exc))
				{
					return false;
				}
				IL_154:
				i++;
			}
			if (!flag)
			{
				if (!tryParse)
				{
					exc = int.GetFormatException();
				}
				return false;
			}
			if (num2 == -1)
			{
				result = (short)((int)num * num2);
			}
			else
			{
				result = num;
			}
			return true;
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x00028A98 File Offset: 0x00026C98
		public static short Parse(string s, IFormatProvider provider)
		{
			return short.Parse(s, NumberStyles.Integer, provider);
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x00028AA4 File Offset: 0x00026CA4
		public static short Parse(string s, NumberStyles style, IFormatProvider provider)
		{
			int num = int.Parse(s, style, provider);
			if (num > 32767 || num < -32768)
			{
				throw new OverflowException("Value too large or too small.");
			}
			return (short)num;
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x00028AE0 File Offset: 0x00026CE0
		public static bool TryParse(string s, out short result)
		{
			Exception ex;
			if (!short.Parse(s, true, out result, out ex))
			{
				result = 0;
				return false;
			}
			return true;
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x00028B04 File Offset: 0x00026D04
		public override string ToString()
		{
			return NumberFormatter.NumberToString((int)this, null);
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x00028B10 File Offset: 0x00026D10
		public string ToString(IFormatProvider provider)
		{
			return NumberFormatter.NumberToString((int)this, provider);
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x00028B1C File Offset: 0x00026D1C
		public string ToString(string format)
		{
			return this.ToString(format, null);
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x00028B28 File Offset: 0x00026D28
		public string ToString(string format, IFormatProvider provider)
		{
			return NumberFormatter.NumberToString(format, this, provider);
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x00028B34 File Offset: 0x00026D34
		public TypeCode GetTypeCode()
		{
			return TypeCode.Int16;
		}

		// Token: 0x0400045B RID: 1115
		public const short MaxValue = 32767;

		// Token: 0x0400045C RID: 1116
		public const short MinValue = -32768;

		// Token: 0x0400045D RID: 1117
		internal short m_value;
	}
}
