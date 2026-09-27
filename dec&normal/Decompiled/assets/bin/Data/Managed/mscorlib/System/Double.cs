using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x020000DF RID: 223
	[ComVisible(true)]
	[Serializable]
	public struct Double : IComparable<double>, IEquatable<double>, IComparable, IConvertible, IFormattable
	{
		// Token: 0x0600089F RID: 2207 RVA: 0x000214F4 File Offset: 0x0001F6F4
		object IConvertible.ToType(Type targetType, IFormatProvider provider)
		{
			if (targetType == null)
			{
				throw new ArgumentNullException("targetType");
			}
			return Convert.ToType(this, targetType, provider, false);
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x00021518 File Offset: 0x0001F718
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this);
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x00021524 File Offset: 0x0001F724
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x00021530 File Offset: 0x0001F730
		char IConvertible.ToChar(IFormatProvider provider)
		{
			throw new InvalidCastException();
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x00021538 File Offset: 0x0001F738
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException();
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x00021540 File Offset: 0x0001F740
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this);
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x0002154C File Offset: 0x0001F74C
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this);
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x00021558 File Offset: 0x0001F758
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x00021564 File Offset: 0x0001F764
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this);
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x00021570 File Offset: 0x0001F770
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x0002157C File Offset: 0x0001F77C
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x00021588 File Offset: 0x0001F788
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this);
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x00021594 File Offset: 0x0001F794
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x000215A0 File Offset: 0x0001F7A0
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x000215AC File Offset: 0x0001F7AC
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x000215B8 File Offset: 0x0001F7B8
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is double))
			{
				throw new ArgumentException(Locale.GetText("Value is not a System.Double"));
			}
			double num = (double)value;
			if (double.IsPositiveInfinity(this) && double.IsPositiveInfinity(num))
			{
				return 0;
			}
			if (double.IsNegativeInfinity(this) && double.IsNegativeInfinity(num))
			{
				return 0;
			}
			if (double.IsNaN(num))
			{
				if (double.IsNaN(this))
				{
					return 0;
				}
				return 1;
			}
			else if (double.IsNaN(this))
			{
				if (double.IsNaN(num))
				{
					return 0;
				}
				return -1;
			}
			else
			{
				if (this > num)
				{
					return 1;
				}
				if (this < num)
				{
					return -1;
				}
				return 0;
			}
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x0002166C File Offset: 0x0001F86C
		public override bool Equals(object obj)
		{
			if (!(obj is double))
			{
				return false;
			}
			double num = (double)obj;
			if (double.IsNaN(num))
			{
				return double.IsNaN(this);
			}
			return num == this;
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x000216A8 File Offset: 0x0001F8A8
		public int CompareTo(double value)
		{
			if (double.IsPositiveInfinity(this) && double.IsPositiveInfinity(value))
			{
				return 0;
			}
			if (double.IsNegativeInfinity(this) && double.IsNegativeInfinity(value))
			{
				return 0;
			}
			if (double.IsNaN(value))
			{
				if (double.IsNaN(this))
				{
					return 0;
				}
				return 1;
			}
			else if (double.IsNaN(this))
			{
				if (double.IsNaN(value))
				{
					return 0;
				}
				return -1;
			}
			else
			{
				if (this > value)
				{
					return 1;
				}
				if (this < value)
				{
					return -1;
				}
				return 0;
			}
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x00021734 File Offset: 0x0001F934
		public bool Equals(double obj)
		{
			if (double.IsNaN(obj))
			{
				return double.IsNaN(this);
			}
			return obj == this;
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x00021758 File Offset: 0x0001F958
		public override int GetHashCode()
		{
			double num = this;
			return num.GetHashCode();
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x00021770 File Offset: 0x0001F970
		public static bool IsInfinity(double d)
		{
			return d == double.PositiveInfinity || d == double.NegativeInfinity;
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x00021790 File Offset: 0x0001F990
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static bool IsNaN(double d)
		{
			return d != d;
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x0002179C File Offset: 0x0001F99C
		public static bool IsNegativeInfinity(double d)
		{
			return d < 0.0 && (d == double.NegativeInfinity || d == double.PositiveInfinity);
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x000217D0 File Offset: 0x0001F9D0
		public static bool IsPositiveInfinity(double d)
		{
			return d > 0.0 && (d == double.NegativeInfinity || d == double.PositiveInfinity);
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x00021804 File Offset: 0x0001FA04
		public static double Parse(string s)
		{
			return double.Parse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, null);
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x00021814 File Offset: 0x0001FA14
		public static double Parse(string s, IFormatProvider provider)
		{
			return double.Parse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, provider);
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x00021824 File Offset: 0x0001FA24
		public static double Parse(string s, NumberStyles style, IFormatProvider provider)
		{
			double result;
			Exception ex;
			if (!double.Parse(s, style, provider, false, out result, out ex))
			{
				throw ex;
			}
			return result;
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x00021848 File Offset: 0x0001FA48
		internal unsafe static bool Parse(string s, NumberStyles style, IFormatProvider provider, bool tryParse, out double result, out Exception exc)
		{
			result = 0.0;
			exc = null;
			if (s == null)
			{
				if (!tryParse)
				{
					exc = new ArgumentNullException("s");
				}
				return false;
			}
			if (s.Length == 0)
			{
				if (!tryParse)
				{
					exc = new FormatException();
				}
				return false;
			}
			if ((style & NumberStyles.AllowHexSpecifier) != NumberStyles.None)
			{
				string text = Locale.GetText("Double doesn't support parsing with '{0}'.", new object[]
				{
					"AllowHexSpecifier"
				});
				throw new ArgumentException(text);
			}
			if (style > NumberStyles.Any)
			{
				if (!tryParse)
				{
					exc = new ArgumentException();
				}
				return false;
			}
			NumberFormatInfo instance = NumberFormatInfo.GetInstance(provider);
			if (instance == null)
			{
				throw new Exception("How did this happen?");
			}
			int length = s.Length;
			int num = 0;
			int i = 0;
			bool flag = (style & NumberStyles.AllowLeadingWhite) != NumberStyles.None;
			bool flag2 = (style & NumberStyles.AllowTrailingWhite) != NumberStyles.None;
			if (flag)
			{
				while (i < length && char.IsWhiteSpace(s[i]))
				{
					i++;
				}
				if (i == length)
				{
					if (!tryParse)
					{
						exc = int.GetFormatException();
					}
					return false;
				}
			}
			int num2 = s.Length - 1;
			if (flag2)
			{
				while (char.IsWhiteSpace(s[num2]))
				{
					num2--;
				}
			}
			if (double.TryParseStringConstant(instance.NaNSymbol, s, i, num2))
			{
				result = double.NaN;
				return true;
			}
			if (double.TryParseStringConstant(instance.PositiveInfinitySymbol, s, i, num2))
			{
				result = double.PositiveInfinity;
				return true;
			}
			if (double.TryParseStringConstant(instance.NegativeInfinitySymbol, s, i, num2))
			{
				result = double.NegativeInfinity;
				return true;
			}
			byte[] array = new byte[length + 1];
			int num3 = 1;
			string text2 = null;
			string text3 = null;
			string text4 = null;
			int num4 = 0;
			int num5 = 0;
			int num6 = 0;
			if ((style & NumberStyles.AllowDecimalPoint) != NumberStyles.None)
			{
				text2 = instance.NumberDecimalSeparator;
				num4 = text2.Length;
			}
			if ((style & NumberStyles.AllowThousands) != NumberStyles.None)
			{
				text3 = instance.NumberGroupSeparator;
				num5 = text3.Length;
			}
			if ((style & NumberStyles.AllowCurrencySymbol) != NumberStyles.None)
			{
				text4 = instance.CurrencySymbol;
				num6 = text4.Length;
			}
			string positiveSign = instance.PositiveSign;
			string negativeSign = instance.NegativeSign;
			while (i < length)
			{
				char c = s[i];
				if (c == '\0')
				{
					i = length;
				}
				else
				{
					switch (num3)
					{
					case 1:
						if ((style & NumberStyles.AllowLeadingSign) != NumberStyles.None)
						{
							if (c == positiveSign[0] && s.Substring(i, positiveSign.Length) == positiveSign)
							{
								num3 = 2;
								i += positiveSign.Length - 1;
								goto IL_624;
							}
							if (c == negativeSign[0] && s.Substring(i, negativeSign.Length) == negativeSign)
							{
								num3 = 2;
								array[num++] = 45;
								i += negativeSign.Length - 1;
								goto IL_624;
							}
						}
						num3 = 2;
						goto IL_304;
					case 2:
						goto IL_304;
					case 3:
						goto IL_429;
					case 4:
						if (char.IsDigit(c))
						{
							num3 = 5;
							goto IL_599;
						}
						if (c == positiveSign[0] && s.Substring(i, positiveSign.Length) == positiveSign)
						{
							num3 = 2;
							i += positiveSign.Length - 1;
							goto IL_624;
						}
						if (c == negativeSign[0] && s.Substring(i, negativeSign.Length) == negativeSign)
						{
							num3 = 2;
							array[num++] = 45;
							i += negativeSign.Length - 1;
							goto IL_624;
						}
						if (char.IsWhiteSpace(c))
						{
							goto IL_5E7;
						}
						if (!tryParse)
						{
							exc = new FormatException("Unknown char: " + c);
						}
						return false;
					case 5:
						goto IL_599;
					case 6:
						goto IL_5E7;
					}
					IL_617:
					if (num3 == 7)
					{
						break;
					}
					goto IL_624;
					IL_429:
					if (char.IsDigit(c))
					{
						array[num++] = (byte)c;
						goto IL_617;
					}
					if (c == 'e' || c == 'E')
					{
						if ((style & NumberStyles.AllowExponent) == NumberStyles.None)
						{
							if (!tryParse)
							{
								exc = new FormatException("Unknown char: " + c);
							}
							return false;
						}
						array[num++] = (byte)c;
						num3 = 4;
						goto IL_617;
					}
					else
					{
						if (char.IsWhiteSpace(c))
						{
							goto IL_5E7;
						}
						if (!tryParse)
						{
							exc = new FormatException("Unknown char: " + c);
						}
						return false;
					}
					IL_304:
					if (char.IsDigit(c))
					{
						array[num++] = (byte)c;
						goto IL_617;
					}
					if (c == 'e' || c == 'E')
					{
						goto IL_429;
					}
					if (num4 > 0 && text2[0] == c && string.CompareOrdinal(s, i, text2, 0, num4) == 0)
					{
						array[num++] = 46;
						i += num4 - 1;
						num3 = 3;
						goto IL_617;
					}
					if (num5 > 0 && text3[0] == c && s.Substring(i, num5) == text3)
					{
						i += num5 - 1;
						num3 = 2;
						goto IL_617;
					}
					if (num6 > 0 && text4[0] == c && s.Substring(i, num6) == text4)
					{
						i += num6 - 1;
						num3 = 2;
						goto IL_617;
					}
					if (char.IsWhiteSpace(c))
					{
						goto IL_5E7;
					}
					if (!tryParse)
					{
						exc = new FormatException("Unknown char: " + c);
					}
					return false;
					IL_599:
					if (char.IsDigit(c))
					{
						array[num++] = (byte)c;
						goto IL_617;
					}
					if (!char.IsWhiteSpace(c))
					{
						if (!tryParse)
						{
							exc = new FormatException("Unknown char: " + c);
						}
						return false;
					}
					IL_5E7:
					if (flag2 && char.IsWhiteSpace(c))
					{
						num3 = 6;
						goto IL_617;
					}
					if (!tryParse)
					{
						exc = new FormatException("Unknown char");
					}
					return false;
				}
				IL_624:
				i++;
			}
			array[num] = 0;
			double num7;
			if (!double.ParseImpl(&array[0], out num7))
			{
				if (!tryParse)
				{
					exc = int.GetFormatException();
				}
				return false;
			}
			if (double.IsPositiveInfinity(num7) || double.IsNegativeInfinity(num7))
			{
				if (!tryParse)
				{
					exc = new OverflowException();
				}
				return false;
			}
			result = num7;
			return true;
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x00021EE4 File Offset: 0x000200E4
		private static bool TryParseStringConstant(string format, string s, int start, int end)
		{
			return end - start + 1 == format.Length && string.CompareOrdinal(format, 0, s, start, format.Length) == 0;
		}

		// Token: 0x060008BC RID: 2236
		[MethodImpl(4096)]
		private unsafe static extern bool ParseImpl(byte* byte_ptr, out double value);

		// Token: 0x060008BD RID: 2237 RVA: 0x00021F0C File Offset: 0x0002010C
		public override string ToString()
		{
			return NumberFormatter.NumberToString(this, null);
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x00021F18 File Offset: 0x00020118
		public string ToString(IFormatProvider provider)
		{
			return NumberFormatter.NumberToString(this, provider);
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x00021F24 File Offset: 0x00020124
		public string ToString(string format, IFormatProvider provider)
		{
			return NumberFormatter.NumberToString(format, this, provider);
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x00021F30 File Offset: 0x00020130
		public TypeCode GetTypeCode()
		{
			return TypeCode.Double;
		}

		// Token: 0x040002EC RID: 748
		public const double Epsilon = 5E-324;

		// Token: 0x040002ED RID: 749
		public const double MaxValue = 1.7976931348623157E+308;

		// Token: 0x040002EE RID: 750
		public const double MinValue = -1.7976931348623157E+308;

		// Token: 0x040002EF RID: 751
		public const double NaN = double.NaN;

		// Token: 0x040002F0 RID: 752
		public const double NegativeInfinity = double.NegativeInfinity;

		// Token: 0x040002F1 RID: 753
		public const double PositiveInfinity = double.PositiveInfinity;

		// Token: 0x040002F2 RID: 754
		private const int State_AllowSign = 1;

		// Token: 0x040002F3 RID: 755
		private const int State_Digits = 2;

		// Token: 0x040002F4 RID: 756
		private const int State_Decimal = 3;

		// Token: 0x040002F5 RID: 757
		private const int State_ExponentSign = 4;

		// Token: 0x040002F6 RID: 758
		private const int State_Exponent = 5;

		// Token: 0x040002F7 RID: 759
		private const int State_ConsumeWhiteSpace = 6;

		// Token: 0x040002F8 RID: 760
		private const int State_Exit = 7;

		// Token: 0x040002F9 RID: 761
		internal double m_value;
	}
}
