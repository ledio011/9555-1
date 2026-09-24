using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading;

namespace System
{
	// Token: 0x02000115 RID: 277
	[ComVisible(true)]
	[Serializable]
	public struct Int64 : IComparable<long>, IEquatable<long>, IComparable, IConvertible, IFormattable
	{
		// Token: 0x06000AE2 RID: 2786 RVA: 0x000299A4 File Offset: 0x00027BA4
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this);
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x000299B0 File Offset: 0x00027BB0
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x000299BC File Offset: 0x00027BBC
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return Convert.ToChar(this);
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x000299C8 File Offset: 0x00027BC8
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			return Convert.ToDateTime(this);
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x000299D4 File Offset: 0x00027BD4
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this);
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x000299E0 File Offset: 0x00027BE0
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this);
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x000299EC File Offset: 0x00027BEC
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x000299F8 File Offset: 0x00027BF8
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this);
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x00029A04 File Offset: 0x00027C04
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x00029A10 File Offset: 0x00027C10
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x00029A1C File Offset: 0x00027C1C
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this);
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x00029A28 File Offset: 0x00027C28
		object IConvertible.ToType(Type targetType, IFormatProvider provider)
		{
			if (targetType == null)
			{
				throw new ArgumentNullException("targetType");
			}
			return Convert.ToType(this, targetType, provider, false);
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x00029A4C File Offset: 0x00027C4C
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x00029A58 File Offset: 0x00027C58
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x00029A64 File Offset: 0x00027C64
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x00029A70 File Offset: 0x00027C70
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is long))
			{
				throw new ArgumentException(Locale.GetText("Value is not a System.Int64"));
			}
			long num = (long)value;
			if (this == num)
			{
				return 0;
			}
			return (this >= num) ? 1 : -1;
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x00029AC0 File Offset: 0x00027CC0
		public override bool Equals(object obj)
		{
			return obj is long && (long)obj == this;
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x00029ADC File Offset: 0x00027CDC
		public override int GetHashCode()
		{
			return (int)(this & (long)((ulong)-1)) ^ (int)(this >> 32);
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x00029AEC File Offset: 0x00027CEC
		public int CompareTo(long value)
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

		// Token: 0x06000AF5 RID: 2805 RVA: 0x00029B04 File Offset: 0x00027D04
		public bool Equals(long obj)
		{
			return obj == this;
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x00029B0C File Offset: 0x00027D0C
		internal static bool Parse(string s, bool tryParse, out long result, out Exception exc)
		{
			long num = 0L;
			int num2 = 1;
			bool flag = false;
			result = 0L;
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
					if (num <= 922337203685477580L)
					{
						if (num != 922337203685477580L)
						{
							num = num * 10L + (long)b;
							flag = true;
							goto IL_166;
						}
						if ((long)b <= 7L || (num2 != 1 && (long)b <= 8L))
						{
							if (num2 == -1)
							{
								num = num * (long)num2 * 10L - (long)b;
							}
							else
							{
								num = num * 10L + (long)b;
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
				IL_166:
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
				result = num * (long)num2;
			}
			else
			{
				result = num;
			}
			return true;
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x00029CCC File Offset: 0x00027ECC
		public static long Parse(string s, IFormatProvider provider)
		{
			return long.Parse(s, NumberStyles.Integer, provider);
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x00029CD8 File Offset: 0x00027ED8
		internal static bool Parse(string s, NumberStyles style, IFormatProvider fp, bool tryParse, out long result, out Exception exc)
		{
			result = 0L;
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
					exc = new FormatException("Input string was not in the correct format: s.Length==0.");
				}
				return false;
			}
			NumberFormatInfo numberFormatInfo = null;
			if (fp != null)
			{
				Type typeFromHandle = typeof(NumberFormatInfo);
				numberFormatInfo = (NumberFormatInfo)fp.GetFormat(typeFromHandle);
			}
			if (numberFormatInfo == null)
			{
				numberFormatInfo = Thread.CurrentThread.CurrentCulture.NumberFormat;
			}
			if (!int.CheckStyle(style, tryParse, ref exc))
			{
				return false;
			}
			bool flag = (style & NumberStyles.AllowCurrencySymbol) != NumberStyles.None;
			bool flag2 = (style & NumberStyles.AllowHexSpecifier) != NumberStyles.None;
			bool flag3 = (style & NumberStyles.AllowThousands) != NumberStyles.None;
			bool flag4 = (style & NumberStyles.AllowDecimalPoint) != NumberStyles.None;
			bool flag5 = (style & NumberStyles.AllowParentheses) != NumberStyles.None;
			bool flag6 = (style & NumberStyles.AllowTrailingSign) != NumberStyles.None;
			bool flag7 = (style & NumberStyles.AllowLeadingSign) != NumberStyles.None;
			bool flag8 = (style & NumberStyles.AllowTrailingWhite) != NumberStyles.None;
			bool flag9 = (style & NumberStyles.AllowLeadingWhite) != NumberStyles.None;
			int num = 0;
			if (flag9 && !int.JumpOverWhite(ref num, s, true, tryParse, ref exc))
			{
				return false;
			}
			bool flag10 = false;
			bool flag11 = false;
			bool flag12 = false;
			bool flag13 = false;
			if (flag5 && s[num] == '(')
			{
				flag10 = true;
				flag12 = true;
				flag11 = true;
				num++;
				if (flag9 && !int.JumpOverWhite(ref num, s, true, tryParse, ref exc))
				{
					return false;
				}
				if (s.Substring(num, numberFormatInfo.NegativeSign.Length) == numberFormatInfo.NegativeSign)
				{
					if (!tryParse)
					{
						exc = new FormatException("Input string was not in the correct format: Has Negative Sign.");
					}
					return false;
				}
				if (s.Substring(num, numberFormatInfo.PositiveSign.Length) == numberFormatInfo.PositiveSign)
				{
					if (!tryParse)
					{
						exc = new FormatException("Input string was not in the correct format: Has Positive Sign.");
					}
					return false;
				}
			}
			if (flag7 && !flag12)
			{
				int.FindSign(ref num, s, numberFormatInfo, ref flag12, ref flag11);
				if (flag12)
				{
					if (flag9 && !int.JumpOverWhite(ref num, s, true, tryParse, ref exc))
					{
						return false;
					}
					if (flag)
					{
						int.FindCurrency(ref num, s, numberFormatInfo, ref flag13);
						if (flag13 && flag9 && !int.JumpOverWhite(ref num, s, true, tryParse, ref exc))
						{
							return false;
						}
					}
				}
			}
			if (flag && !flag13)
			{
				int.FindCurrency(ref num, s, numberFormatInfo, ref flag13);
				if (flag13)
				{
					if (flag9 && !int.JumpOverWhite(ref num, s, true, tryParse, ref exc))
					{
						return false;
					}
					if (flag13 && !flag12 && flag7)
					{
						int.FindSign(ref num, s, numberFormatInfo, ref flag12, ref flag11);
						if (flag12 && flag9 && !int.JumpOverWhite(ref num, s, true, tryParse, ref exc))
						{
							return false;
						}
					}
				}
			}
			long num2 = 0L;
			int num3 = 0;
			bool flag14 = false;
			do
			{
				if (!int.ValidDigit(s[num], flag2))
				{
					if (!flag3 || (!int.FindOther(ref num, s, numberFormatInfo.NumberGroupSeparator) && !int.FindOther(ref num, s, numberFormatInfo.CurrencyGroupSeparator)))
					{
						if (flag14 || !flag4 || (!int.FindOther(ref num, s, numberFormatInfo.NumberDecimalSeparator) && !int.FindOther(ref num, s, numberFormatInfo.CurrencyDecimalSeparator)))
						{
							break;
						}
						flag14 = true;
					}
				}
				else if (flag2)
				{
					num3++;
					char c = s[num++];
					int num4;
					if (char.IsDigit(c))
					{
						num4 = (int)(c - '0');
					}
					else if (char.IsLower(c))
					{
						num4 = (int)(c - 'a' + '\n');
					}
					else
					{
						num4 = (int)(c - 'A' + '\n');
					}
					ulong num5 = (ulong)num2;
					try
					{
						num2 = (long)(checked(num5 * 16UL + (ulong)num4));
					}
					catch (OverflowException ex)
					{
						if (!tryParse)
						{
							exc = ex;
						}
						return false;
					}
				}
				else if (flag14)
				{
					num3++;
					if (s[num++] != '0')
					{
						goto Block_49;
					}
				}
				else
				{
					num3++;
					try
					{
						num2 = checked(num2 * 10L - unchecked((long)(checked(s[num++] - '0'))));
					}
					catch (OverflowException)
					{
						if (!tryParse)
						{
							exc = new OverflowException("Value too large or too small.");
						}
						return false;
					}
				}
			}
			while (num < s.Length);
			goto IL_462;
			Block_49:
			if (!tryParse)
			{
				exc = new OverflowException("Value too large or too small.");
			}
			return false;
			IL_462:
			if (num3 == 0)
			{
				if (!tryParse)
				{
					exc = new FormatException("Input string was not in the correct format: nDigits == 0.");
				}
				return false;
			}
			if (flag6 && !flag12)
			{
				int.FindSign(ref num, s, numberFormatInfo, ref flag12, ref flag11);
				if (flag12)
				{
					if (flag8 && !int.JumpOverWhite(ref num, s, true, tryParse, ref exc))
					{
						return false;
					}
					if (flag)
					{
						int.FindCurrency(ref num, s, numberFormatInfo, ref flag13);
					}
				}
			}
			if (flag && !flag13)
			{
				if (numberFormatInfo.CurrencyPositivePattern == 3 && s[num++] != ' ')
				{
					if (tryParse)
					{
						return false;
					}
					throw new FormatException("Input string was not in the correct format: no space between number and currency symbol.");
				}
				else
				{
					int.FindCurrency(ref num, s, numberFormatInfo, ref flag13);
					if (flag13 && num < s.Length)
					{
						if (flag8 && !int.JumpOverWhite(ref num, s, true, tryParse, ref exc))
						{
							return false;
						}
						if (!flag12 && flag6)
						{
							int.FindSign(ref num, s, numberFormatInfo, ref flag12, ref flag11);
						}
					}
				}
			}
			if (flag8 && num < s.Length && !int.JumpOverWhite(ref num, s, false, tryParse, ref exc))
			{
				return false;
			}
			if (flag10)
			{
				if (num >= s.Length || s[num++] != ')')
				{
					if (!tryParse)
					{
						exc = new FormatException("Input string was not in the correct format: No room for close parens.");
					}
					return false;
				}
				if (flag8 && num < s.Length && !int.JumpOverWhite(ref num, s, false, tryParse, ref exc))
				{
					return false;
				}
			}
			if (num < s.Length && s[num] != '\0')
			{
				if (!tryParse)
				{
					exc = new FormatException(string.Concat(new object[]
					{
						"Input string was not in the correct format: Did not parse entire string. pos = ",
						num,
						" s.Length = ",
						s.Length
					}));
				}
				return false;
			}
			if (!flag11 && !flag2)
			{
				try
				{
					num2 = (long)(checked(unchecked((ulong)0) - (ulong)num2));
				}
				catch (OverflowException ex2)
				{
					if (!tryParse)
					{
						exc = ex2;
					}
					return false;
				}
			}
			result = num2;
			return true;
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x0002A390 File Offset: 0x00028590
		public static long Parse(string s)
		{
			long result;
			Exception ex;
			if (!long.Parse(s, false, out result, out ex))
			{
				throw ex;
			}
			return result;
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x0002A3B0 File Offset: 0x000285B0
		public static long Parse(string s, NumberStyles style, IFormatProvider provider)
		{
			long result;
			Exception ex;
			if (!long.Parse(s, style, provider, false, out result, out ex))
			{
				throw ex;
			}
			return result;
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x0002A3D4 File Offset: 0x000285D4
		public static bool TryParse(string s, out long result)
		{
			Exception ex;
			if (!long.Parse(s, true, out result, out ex))
			{
				result = 0L;
				return false;
			}
			return true;
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x0002A3F8 File Offset: 0x000285F8
		public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out long result)
		{
			Exception ex;
			if (!long.Parse(s, style, provider, true, out result, out ex))
			{
				result = 0L;
				return false;
			}
			return true;
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x0002A420 File Offset: 0x00028620
		public override string ToString()
		{
			return NumberFormatter.NumberToString(this, null);
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x0002A42C File Offset: 0x0002862C
		public string ToString(IFormatProvider provider)
		{
			return NumberFormatter.NumberToString(this, provider);
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x0002A438 File Offset: 0x00028638
		public string ToString(string format)
		{
			return this.ToString(format, null);
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x0002A444 File Offset: 0x00028644
		public string ToString(string format, IFormatProvider provider)
		{
			return NumberFormatter.NumberToString(format, this, provider);
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x0002A450 File Offset: 0x00028650
		public TypeCode GetTypeCode()
		{
			return TypeCode.Int64;
		}

		// Token: 0x04000461 RID: 1121
		public const long MaxValue = 9223372036854775807L;

		// Token: 0x04000462 RID: 1122
		public const long MinValue = -9223372036854775808L;

		// Token: 0x04000463 RID: 1123
		internal long m_value;
	}
}
