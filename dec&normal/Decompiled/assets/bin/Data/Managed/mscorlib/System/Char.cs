using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x02000074 RID: 116
	[ComVisible(true)]
	[Serializable]
	public struct Char : IComparable<char>, IEquatable<char>, IComparable, IConvertible
	{
		// Token: 0x06000399 RID: 921 RVA: 0x000124CC File Offset: 0x000106CC
		static Char()
		{
			char.GetDataTablePointers(out char.category_data, out char.numeric_data, out char.numeric_data_values, out char.to_lower_data_low, out char.to_lower_data_high, out char.to_upper_data_low, out char.to_upper_data_high);
		}

		// Token: 0x0600039A RID: 922 RVA: 0x000124F8 File Offset: 0x000106F8
		object IConvertible.ToType(Type targetType, IFormatProvider provider)
		{
			if (targetType == null)
			{
				throw new ArgumentNullException("targetType");
			}
			return Convert.ToType(this, targetType, provider, false);
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0001251C File Offset: 0x0001071C
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			throw new InvalidCastException();
		}

		// Token: 0x0600039C RID: 924 RVA: 0x00012524 File Offset: 0x00010724
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		// Token: 0x0600039D RID: 925 RVA: 0x00012530 File Offset: 0x00010730
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return this;
		}

		// Token: 0x0600039E RID: 926 RVA: 0x00012534 File Offset: 0x00010734
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException();
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0001253C File Offset: 0x0001073C
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			throw new InvalidCastException();
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x00012544 File Offset: 0x00010744
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			throw new InvalidCastException();
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0001254C File Offset: 0x0001074C
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x00012558 File Offset: 0x00010758
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this);
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x00012564 File Offset: 0x00010764
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x00012570 File Offset: 0x00010770
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0001257C File Offset: 0x0001077C
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			throw new InvalidCastException();
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x00012584 File Offset: 0x00010784
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x00012590 File Offset: 0x00010790
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0001259C File Offset: 0x0001079C
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		// Token: 0x060003A9 RID: 937
		[MethodImpl(4096)]
		private unsafe static extern void GetDataTablePointers(out byte* category_data, out byte* numeric_data, out double* numeric_data_values, out ushort* to_lower_data_low, out ushort* to_lower_data_high, out ushort* to_upper_data_low, out ushort* to_upper_data_high);

		// Token: 0x060003AA RID: 938 RVA: 0x000125A8 File Offset: 0x000107A8
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is char))
			{
				throw new ArgumentException(Locale.GetText("Value is not a System.Char"));
			}
			char c = (char)value;
			if (this == c)
			{
				return 0;
			}
			if (this > c)
			{
				return 1;
			}
			return -1;
		}

		// Token: 0x060003AB RID: 939 RVA: 0x000125F4 File Offset: 0x000107F4
		public override bool Equals(object obj)
		{
			return obj is char && (char)obj == this;
		}

		// Token: 0x060003AC RID: 940 RVA: 0x00012610 File Offset: 0x00010810
		public int CompareTo(char value)
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

		// Token: 0x060003AD RID: 941 RVA: 0x00012628 File Offset: 0x00010828
		public bool Equals(char obj)
		{
			return this == obj;
		}

		// Token: 0x060003AE RID: 942 RVA: 0x00012630 File Offset: 0x00010830
		public override int GetHashCode()
		{
			return (int)this;
		}

		// Token: 0x060003AF RID: 943 RVA: 0x00012634 File Offset: 0x00010834
		public unsafe static UnicodeCategory GetUnicodeCategory(char c)
		{
			return (UnicodeCategory)char.category_data[c];
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x00012640 File Offset: 0x00010840
		public unsafe static bool IsDigit(char c)
		{
			return char.category_data[c] == 8;
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x00012650 File Offset: 0x00010850
		public unsafe static bool IsLetter(char c)
		{
			return char.category_data[c] <= 4;
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x00012660 File Offset: 0x00010860
		public unsafe static bool IsLetterOrDigit(char c)
		{
			int num = (int)char.category_data[c];
			return num <= 4 || num == 8;
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x00012684 File Offset: 0x00010884
		public static bool IsLetterOrDigit(string s, int index)
		{
			char.CheckParameter(s, index);
			return char.IsLetterOrDigit(s[index]);
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0001269C File Offset: 0x0001089C
		public unsafe static bool IsLower(char c)
		{
			return char.category_data[c] == 1;
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x000126AC File Offset: 0x000108AC
		public unsafe static bool IsSurrogate(char c)
		{
			return char.category_data[c] == 16;
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x000126BC File Offset: 0x000108BC
		public unsafe static bool IsUpper(char c)
		{
			return char.category_data[c] == 0;
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x000126CC File Offset: 0x000108CC
		public unsafe static bool IsWhiteSpace(char c)
		{
			int num = (int)char.category_data[c];
			return num > 10 && (num <= 13 || (c >= '\t' && c <= '\r') || c == '\u0085' || c == '\u205f');
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0001271C File Offset: 0x0001091C
		private static void CheckParameter(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index < 0 || index >= s.Length)
			{
				throw new ArgumentOutOfRangeException(Locale.GetText("The value of index is less than zero, or greater than or equal to the length of s."));
			}
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x00012754 File Offset: 0x00010954
		public static char Parse(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (s.Length != 1)
			{
				throw new FormatException(Locale.GetText("s contains more than one character."));
			}
			return s[0];
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0001278C File Offset: 0x0001098C
		public static char ToLower(char c)
		{
			return CultureInfo.CurrentCulture.TextInfo.ToLower(c);
		}

		// Token: 0x060003BB RID: 955 RVA: 0x000127A0 File Offset: 0x000109A0
		public unsafe static char ToLowerInvariant(char c)
		{
			if (c <= 'Ⓩ')
			{
				return (char)char.to_lower_data_low[c];
			}
			if (c >= 'Ａ')
			{
				return (char)char.to_lower_data_high[c - 'Ａ'];
			}
			return c;
		}

		// Token: 0x060003BC RID: 956 RVA: 0x000127D8 File Offset: 0x000109D8
		public static char ToLower(char c, CultureInfo culture)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			if (culture.LCID == 127)
			{
				return char.ToLowerInvariant(c);
			}
			return culture.TextInfo.ToLower(c);
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0001280C File Offset: 0x00010A0C
		public static char ToUpper(char c)
		{
			return CultureInfo.CurrentCulture.TextInfo.ToUpper(c);
		}

		// Token: 0x060003BE RID: 958 RVA: 0x00012820 File Offset: 0x00010A20
		public unsafe static char ToUpperInvariant(char c)
		{
			if (c <= 'ⓩ')
			{
				return (char)char.to_upper_data_low[c];
			}
			if (c >= 'Ａ')
			{
				return (char)char.to_upper_data_high[c - 'Ａ'];
			}
			return c;
		}

		// Token: 0x060003BF RID: 959 RVA: 0x00012858 File Offset: 0x00010A58
		public override string ToString()
		{
			return new string(this, 1);
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x00012864 File Offset: 0x00010A64
		public string ToString(IFormatProvider provider)
		{
			return new string(this, 1);
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x00012870 File Offset: 0x00010A70
		public TypeCode GetTypeCode()
		{
			return TypeCode.Char;
		}

		// Token: 0x040001B9 RID: 441
		public const char MaxValue = '￿';

		// Token: 0x040001BA RID: 442
		public const char MinValue = '\0';

		// Token: 0x040001BB RID: 443
		internal char m_value;

		// Token: 0x040001BC RID: 444
		private unsafe static readonly byte* category_data;

		// Token: 0x040001BD RID: 445
		private unsafe static readonly byte* numeric_data;

		// Token: 0x040001BE RID: 446
		private unsafe static readonly double* numeric_data_values;

		// Token: 0x040001BF RID: 447
		private unsafe static readonly ushort* to_lower_data_low;

		// Token: 0x040001C0 RID: 448
		private unsafe static readonly ushort* to_lower_data_high;

		// Token: 0x040001C1 RID: 449
		private unsafe static readonly ushort* to_upper_data_low;

		// Token: 0x040001C2 RID: 450
		private unsafe static readonly ushort* to_upper_data_high;
	}
}
